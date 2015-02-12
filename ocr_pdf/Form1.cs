using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace ocr_pdf
{
    public partial class Form1 : Form
    {
        BackgroundWorker bgwPDF = new BackgroundWorker();       
        public string gs_path, gs_param, tesseract_path, tesseract_param;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //crea la dialog di apertura file
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "File PDF|*.pdf";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.Multiselect = true;
            DialogResult userClickedOK = openFileDialog1.ShowDialog();

            //inizia l'elaborazione
            if (userClickedOK == DialogResult.OK)//se non è stato annullato inizia il lavoro
            {
                button1.Enabled = false;//disattiva i comandi mentre gira il background worker
                chkbx_delete.Enabled = false;
                btn_edit_conf.Enabled = false;
                lbl_progrtot.Text = "Elaborazione";

                prgbar_tot.Maximum = openFileDialog1.FileNames.GetUpperBound(0);//configura la progress bar
                prgbar_tot.Value = 0;
                bgwPDF.DoWork += new DoWorkEventHandler(bgwPDF_DoWork);
                bgwPDF.ProgressChanged += new ProgressChangedEventHandler(bgwPDF_ProgressChanged);
                bgwPDF.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgwPDF_RunWorkerCompleted);
                bgwPDF.WorkerReportsProgress = true;
                bgwPDF.RunWorkerAsync(openFileDialog1.FileNames);//avvia la conversione in background
            }
        }

        void bgwPDF_DoWork(object sender, DoWorkEventArgs e)//background worker per la conversione dei file
        {
            string fileout;
            FileInfo fileinfo_fileout;
            //configura i processi di ghostscript
            Process gs_proc = new Process();
            Process tesseract_proc = new Process();
            string tempfile = Path.GetTempPath() + "ocr_pdf.tif";//definisce il file temporaneo di lavoro
            gs_proc.StartInfo.RedirectStandardOutput = true;
            gs_proc.StartInfo.UseShellExecute = false;
            gs_proc.StartInfo.CreateNoWindow = true;
            gs_proc.StartInfo.FileName = gs_path;//e tesseract
            tesseract_proc.StartInfo.RedirectStandardOutput = true;
            tesseract_proc.StartInfo.UseShellExecute = false;
            tesseract_proc.StartInfo.CreateNoWindow = true;
            tesseract_proc.StartInfo.FileName = tesseract_path;

            int percentuale, contatore=0;//variabili per progress bar

            string[] lista_file = (string[])e.Argument;
            foreach (String file in lista_file)
            {
                fileout = Path.GetDirectoryName(file) + Path.DirectorySeparatorChar + Path.GetFileNameWithoutExtension(file) + "_ocr"; //genera il nome del file di output, l'estensione la mette tesseract in automatico
                gs_proc.StartInfo.Arguments = gs_param + "-sOutputFile=" + tempfile + " " + file;
                gs_proc.Start();//converte il file di origine in tiff per processarlo con tesseract
                gs_proc.WaitForExit();//attende la fine della conversione con ghostscript
                if (File.Exists(tempfile))
                {//se il file temporaneo è stato creato lo processa
                    tesseract_proc.StartInfo.Arguments = tempfile + " " + fileout + " " + tesseract_param;//esegue tesseract e crea il file di output
                    tesseract_proc.Start();
                    tesseract_proc.WaitForExit();//attende la fine del processo di tesseract
                    File.Delete(tempfile);//elimina il file temporaneo
                    fileinfo_fileout = new FileInfo(fileout + ".pdf");//controlla che l'output non sia di 0 byte
                    if (fileinfo_fileout.Length == 0)
                    {//se è 0 il file non è stato creato correttamente
                        //MessageBox.Show("Il file " + file + " non è stato convertito\nErrore nella creazione file pdf");
                        File.Delete(fileout + ".pdf");//elimina il file di uscita non completo
                    }
                    else
                    {//se il file di uscita è valido
                        if (chkbx_delete.Checked)
                        {//se la check box di eliminazione file è selezionata
                            File.Delete(file);//elimina il file sorgente
                        }
                    }
                }
                else
                { //altrimenti mostra messagebox di errore del lavoro di ghostscript
                    //MessageBox.Show("Il file " + file + " non è stato convertito\nErrore nella creazione file temporaneo");
                }
                contatore++;
                percentuale = (contatore * 100) / (lista_file.GetUpperBound(0) + 1);
                bgwPDF.ReportProgress(percentuale, contatore);
                }
        }

        void bgwPDF_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            prgbar_tot.Minimum = 0;
            prgbar_tot.Maximum = 100;
            prgbar_tot.Value = e.ProgressPercentage;
            lbl_progrtot.Text = string.Format("{0} file elaborati", e.UserState);
        }

        void bgwPDF_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Enabled = true;//riattiva i comandi mentre gira il background worker
            chkbx_delete.Enabled = true;
            btn_edit_conf.Enabled = true;
            lbl_progrtot.Text = "Completato";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cfg_file_funct("load");
            chkbx_delete.Checked = true;
            lbl_progrtot.Text = "";
        }

        private void btn_edit_conf_Click(object sender, EventArgs e)
        {
            cfg_file_funct("edit");
        }

        public int cfg_file_funct(string mode)
        {
            string config_file = Application.StartupPath + Path.DirectorySeparatorChar + "config.txt";
            switch (mode)//definisce la modalità operativa
            {
                case "load"://legge il file di configurazione
                    if (File.Exists(config_file))//controlla se esiste il file di configurazione
                    {
                        cfg_file_funct("enable_pdfprocess");//abilita le funzioni per processare i pdf
                        string[] lines = System.IO.File.ReadAllLines(config_file);//se c'è lo legge
                        foreach (string line in lines)//processa i parametri
                        {
                            switch (line.Split('!')[0])//cerca il parametro scritto prima del !
                            {//cerca il valore del parametro scritto tra il ! e il ;
                                case "GS_PATH"://percorso dell'eseguibile di ghostscript
                                    gs_path = line.Split('!')[1].Split(';')[0];
                                    break;
                                case "GS_PARAM"://parametri di ghostscript
                                    gs_param = line.Split('!')[1].Split(';')[0];
                                    break;
                                case "TESSERACT_PATH"://percorso dell'eseguibile di tesseract
                                    tesseract_path = line.Split('!')[1].Split(';')[0];
                                    break;
                                case "TESSERACT_PARAM"://parametri di tesseract
                                    tesseract_param = line.Split('!')[1].Split(';')[0];
                                    break;
                                default://tutto il resto: parametri non validi, commenti etc.
                                    break;
                            }
                        }
                        //controlla che i percorsi di ghostscript e tesseract siano validi
                        //se non sono validi disattiva le funzioni per processare i pdf
                        if (!File.Exists(gs_path))
                        {
                            cfg_file_funct("disable_pdfprocess");
                            MessageBox.Show("Non trovo ghostscript:\n" + gs_path);
                        }
                        if (!File.Exists(tesseract_path))
                        {
                            cfg_file_funct("disable_pdfprocess");
                            MessageBox.Show("Non trovo tesseract:\n" + tesseract_path);
                        }
                    }
                    else
                    {//se non trova il file di configurazione
                        MessageBox.Show("Non trovo il file di configurazione: " + config_file);
                        cfg_file_funct("disable_all");//disattiva tutto il programma
                    }
            break;
                case "edit":
                    Process editcfg = Process.Start(config_file);//avvia l'editor predefinito per il file di configurazione
                    editcfg.WaitForExit();//attende che si chiuda il file di configurazione
                    cfg_file_funct("load");//chiama se stessa per rileggere il file di configurazione
            break;
                case "disable_all"://disattiva tutti i bottoni
                        button1.Enabled = false;
                        btn_edit_conf.Enabled = false;
            break;
                case "disable_pdfprocess"://disattiva il bottone per processare i pdf
                        button1.Enabled = false;
            break;
                case "enable_pdfprocess"://attiva il bottone per processare i pdf
                        button1.Enabled = true;
            break;
                default:
            break;
        }
            return 0;
        }


    }
}
