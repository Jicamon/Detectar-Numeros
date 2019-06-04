using System;

namespace Detectar_numeros
{
    class RedNeuronal
    {
        public double[] entrada = new double[35];

        public double[] oculta = new double[20];
        public double[] ocultaSP = new double[20];

        public double[] salida = new double[10];
        public double[] salidaSP = new double[10];


        public double[,] pesosE = new double[35,20];
        public double[,] pesosECopia = new double[35,20];
        public double[,] pesosS = new double[20,10];
        public double[,] pesosSCopia = new double[20,10];

        public RedNeuronal(double[] entradas){
            
            initPesos();
            initNeuronas(entradas);
            //feedForward(entrada);

        }

        public void initPesos(){
            Random rand = new Random();
            double pesoE;
            for (int i = 0; i < 35; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    pesoE = rand.NextDouble();
                    pesosE[i,j] = pesoE;
                    pesosECopia[i,j] = pesoE;
                }
            }
            double pesoS;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    pesoS = rand.NextDouble();
                    pesosS[i,j] = pesoS;
                    pesosSCopia[i,j] = pesoS;
                }
            }

        }

        public void initNeuronas(double[] ent){

            for (int i = 0; i < ent.Length; i++)
            {
                entrada[i] = ent[i];
            } 

        }

        public double[] feedForward(double[] ent){
            double sumatoria = 0;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 35; j++)
                {
                    sumatoria += pesosE[j,i] * ent[j];
                }
                
                ocultaSP[i] = sumatoria;
                oculta[i] = 1/(1 + Math.Exp(-sumatoria));
                sumatoria = 0;
            }

            sumatoria = 0;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    sumatoria += pesosE[j,i] * oculta[j];
                }
                salidaSP[i] = sumatoria;
                salida[i] = 1 / (1 + Math.Exp(-sumatoria));
                sumatoria = 0;
            }

            return salida;

        }

        public double[] getEntrada(){
            return entrada;
        }

        public double[] getOculta(){
            return oculta;
        }

        public double[] getOcultaSP(){
            return ocultaSP;
        }
        public double[] getSalida(){
            return salida;
        }

        public double[] getSalidaSP(){
            return salidaSP;
        }

        public double[,] getPesosE(){
            return pesosE;
        }

        public double[,] getPesosS(){
            return pesosS;
        }

        public double[,] getPesosECopia(){
            return pesosECopia;
        }

        public double[,] getPesosSCopia(){
            return pesosSCopia;
        }
    }
}
