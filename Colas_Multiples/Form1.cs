using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Colas_Multiples
{
    public partial class Form1 : Form
    {
        int ID = 1;
        delegate void delegadoFCFS();

        List<Proceso> FCFS = new List<Proceso>();
        List<Proceso> SJF = new List<Proceso>();
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            int Tiempo;
            if (!int.TryParse(TxtTiempo.Text, out Tiempo))
            {
                MessageBox.Show("Valor de Tiempo Invalido");
                return;
            }
            Proceso p = new Proceso(ID, Tiempo);
            ID++;
            if (CbSFJ.Checked == true)
                SJF.Add(p);
            else
                FCFS.Add(p);
            TxtTiempo.Text = "";
            BtnIniciar.Enabled = true;
        }

        private void BtnIniciar_Click(object sender, EventArgs e)
        {
            BtnAgregar.Enabled = false;
            BtnIniciar.Enabled = false;
            int n = SJF.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (SJF[j].GetTiempo() > SJF[j + 1].GetTiempo())
                    {
                        Proceso Temporal = new Proceso();
                        Temporal = SJF[j];
                        SJF[j] = SJF[j + 1];
                        SJF[j + 1] = Temporal;
                    }
                }
            }
            Thread t1 = new Thread(HiloFCFS);
            Thread t2 = new Thread(HiloSJF);
            t1.Start();
            t2.Start();
            ID = 1;
            BtnAgregar.Enabled = true;
            BtnIniciar.Enabled = true;
        }
        public void HiloFCFS()
        {
            int Contador;
            while (FCFS.Count != 0)
            {
                if (FCFS.Count != 0)
                {
                    Contador = FCFS[0].GetTiempoTranscurrido();
                    delegadoFCFS MD = new delegadoFCFS(ActualizarPbFCFS);
                    this.Invoke(MD, new object[] { });
                    if (Contador == FCFS[0].GetTiempo())
                    {
                        FCFS.RemoveAt(0);
                        Thread.Sleep(300);
                        delegadoFCFS ND = new delegadoFCFS(ReiniciarPbFCFS);
                        this.Invoke(ND, new object[] { });
                    }
                    else
                        FCFS[0].SetTiempoTranscurrido(Contador + 1);
                }
                Thread.Sleep(500);
            }
        }

        public void ActualizarPbFCFS()
        {
            PbFCFS.Maximum = FCFS[0].GetTiempo();
            PbFCFS.Step = 1;
            PbFCFS.PerformStep();
            PbFCFS.Refresh();
        }

        public void ReiniciarPbFCFS()
        {
            PbFCFS.Value = 0;
        }

        public void HiloSJF()
        {
            int Contador;
            while (SJF.Count != 0)
            {
                if (SJF.Count != 0)
                {
                    Contador = SJF[0].GetTiempoTranscurrido();
                    delegadoFCFS MD = new delegadoFCFS(ActualizarPbSJF);
                    this.Invoke(MD, new object[] { });
                    if (Contador == SJF[0].GetTiempo())
                    {
                        SJF.RemoveAt(0);
                        Thread.Sleep(300);
                        delegadoFCFS ND = new delegadoFCFS(ReiniciarPbSJF);
                        this.Invoke(ND, new object[] { });
                    }
                    else
                        SJF[0].SetTiempoTranscurrido(Contador + 1);
                }
                Thread.Sleep(500);
            }
        }

        public void ActualizarPbSJF()
        {
            PbSJF.Maximum = SJF[0].GetTiempo();
            PbSJF.Step = 1;
            PbSJF.PerformStep();
            PbSJF.Refresh();
        }

        public void ReiniciarPbSJF()
        {
            PbSJF.Value = 0;
        }
    }
}
