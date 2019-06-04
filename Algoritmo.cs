using System;
using System.Linq;

namespace Detectar_numeros{
    class Algoritmo{
        
        public static double[][] bd = new double[10][];

        public static void setBD(){
            bd[0] = new double[] {0,1,1,1,0,
                                  1,0,0,0,1,
                                  1,0,0,0,1,
                                  1,0,0,0,1,
                                  1,0,0,0,1,
                                  1,0,0,0,1,
                                  0,1,1,1,0};

            bd[1] = new double[] {0,0,1,0,0,
                                  0,1,1,0,0,
                                  1,0,1,0,0,
                                  0,0,1,0,0,
                                  0,0,1,0,0,
                                  0,0,1,0,0,
                                  1,1,1,1,1};

            bd[2] = new double[] {0,1,1,1,0,
                                  1,0,0,0,1,
                                  0,0,0,1,0,
                                  0,0,1,0,0,
                                  0,1,0,0,0,
                                  1,0,0,0,0,
                                  1,1,1,1,1};
            
            bd[3] = new double[] {0,1,1,1,0,
                                  1,0,0,0,1,
                                  0,0,0,0,1,
                                  0,0,1,1,0,
                                  0,0,0,0,1,
                                  1,0,0,0,1,
                                  0,1,1,1,0};

            bd[4] = new double[] {0,0,1,1,0,
                                  0,1,0,1,0,
                                  1,0,0,1,0,
                                  1,1,1,1,1,
                                  0,0,0,1,0,
                                  0,0,0,1,0,
                                  0,0,0,1,0}; 

            bd[5] = new double[] {1,1,1,1,1,
                                  1,0,0,0,0,
                                  1,0,0,0,0,
                                  1,1,1,1,0,
                                  0,0,0,0,1,
                                  0,0,0,0,1,
                                  1,1,1,1,0};                                                                
            
            bd[6] = new double[] {0,1,1,1,0,
                                  1,0,0,0,0,
                                  1,0,0,0,0,
                                  1,1,1,1,0,
                                  1,0,0,0,1,
                                  1,0,0,0,1,
                                  0,1,1,1,0};

            bd[7] = new double[] {1,1,1,1,1,
                                  0,0,0,0,1,
                                  0,0,0,0,1,
                                  0,0,0,1,0,
                                  0,0,1,0,0,
                                  0,1,0,0,0,
                                  1,0,0,0,0};    
                                  
            bd[8] = new double[] {0,1,1,1,0,
                                  1,0,0,0,1,
                                  1,0,0,0,1,
                                  0,1,1,1,0,
                                  1,0,0,0,1,
                                  1,0,0,0,1,
                                  0,1,1,1,0};                                                                                                   

            bd[9] = new double[] {0,1,1,1,0,
                                  1,0,0,0,1,
                                  1,0,0,0,1,
                                  0,1,1,1,1,
                                  0,0,0,0,1,
                                  0,0,0,0,1,
                                  0,1,1,1,0};
        }
        
            
        public static void wea(){    
            
            double[] calar = new double[]  {0,1,1,1,0,
                                            1,0,0,0,1,
                                            1,0,0,0,1,
                                            0,1,1,1,1,
                                            0,0,0,0,1,
                                            0,0,0,0,1,
                                            0,1,1,1,0};
            Random random = new Random();
            
            setBD();
            RedNeuronal rn = new RedNeuronal(bd[0]);
            int tamaño_entrada = rn.getEntrada().Length;
            int tamaño_oculta = rn.getOculta().Length;
            int tamaño_salida = rn.getSalida().Length;

            
            Console.WriteLine("Hora de inicio: " + DateTime.Now);
            train(rn, bd);
            Console.WriteLine("Entrenamiento terminado: " + DateTime.Now);
            
            //rn.feedForward(calar);

            

            //rn.feedForward(bd[0]);

            //prtNeuEntrada(rn, tamaño_entrada);

            //prtNeuOculta(rn, tamaño_oculta);

            //prtNeuSalida(rn, tamaño_salida);
            
            //prtPesos1(rn);

            //prtPesos2(rn);

            //Console.WriteLine("Error: " + obtenerError(rn.feedForward(bd[0])[0], 0, 0));

            //for (int i = 0; i < rn.feedForward(bd[0]).Length; i++)
            //{
            //     obtenerError(rn ,rn.feedForward(bd[0])[i], i, 0);
            //}

            double[] prueba = new double[] {1,1,1,1,1,
                                            1,0,0,1,0,
                                            0,0,1,0,0,
                                            0,0,1,0,0,
                                            0,0,1,0,0,
                                            0,1,0,0,0,
                                            1,0,0,0,0};
                                            
            Console.WriteLine("Es un " + especular(prueba));

        }
        
        public static void train(RedNeuronal rn, double[][] entrenador){
            double error = .99; 
            for (int i = 0; i < entrenador.Length; i++)
            {   
                
                do{
                    for (int j = 0; j < rn.getSalida().Length; j++)
                    {
                        error = obtenerError(rn,rn.feedForward(entrenador[i])[j], j, i);
                        Console.WriteLine("Error "+ (decimal)error);
                        if(Math.Abs(error) > .00000001){
                            ajustarPesosBP(rn, error, i);
                        }
                    }
                    
                }
                while(Math.Abs(error) > .00000001);                       
            }
        }

        public static void ajustarPesosBP(RedNeuronal rn, double error, int neuSalida){
                double[] oculta = rn.getOculta();
                int tamaño_oculta = oculta.Length;
                double[] WD = new double[tamaño_oculta];
                double[] SOD = new double[tamaño_oculta];  
                double[] PesosDWD = new double[rn.getPesosE().Length];

                double[] entrada = (double[])rn.getEntrada().Clone();
                double[,] PreChanges = (double[,])rn.getPesosSCopia().Clone();
                double[] ocultaSP = rn.getOcultaSP();
                
                for (int i = 0; i < WD.Length; i++)
                {
                    WD[i] = oculta[i] * error;
                }

                double[,] pesos2 = rn.getPesosS();

                for (int i = 0; i < WD.Length; i++)
                {
                    pesos2[i,neuSalida] = WD[i];
                }
                double pesoPrimerCapa = 0;
                for (int i = 0; i < WD.Length; i++)
                {
                    pesoPrimerCapa = PreChanges[i,neuSalida] * error;
                    pesoPrimerCapa *= ocultaSP[i];
                    SOD[i] = pesoPrimerCapa;
                }  

                for (int i = 0; i < oculta.Length; i++)
                {
                    for (int j = 0; j < entrada.Length; j++)
                    {
                        rn.pesosE[j,i] += SOD[i] * entrada[i];
                    }
                }

        }

        public static double obtenerError(RedNeuronal rn, double salida,int neuSal, int esperado){
            double elError = 0;    
            double sinProcesar = rn.getSalidaSP()[neuSal];
            double laOtraWea = (Math.Exp(-sinProcesar)/Math.Pow(1 + Math.Exp(-sinProcesar),-2));
            if(neuSal == esperado){
                elError =  1 - salida;
            }else{
                elError =  0 - salida;
            }
            
            return elError * laOtraWea;
        }

        private static void prtNeuEntrada(RedNeuronal rn, int tamaño_entrada)
        {
            Console.WriteLine("########################## ENTRADA ##########################");

            for (int i = 0; i < tamaño_entrada; i++)
            {
                Console.WriteLine("Valor de la neurona {0}: " + rn.getEntrada()[i], i + 1);
            }
        }

        public static void prtNeuOculta(RedNeuronal rn, int tamaño_oculta){
            Console.WriteLine("########################## OCULTA ##########################");

            for (int i = 0; i < tamaño_oculta; i++)
            {
                Console.WriteLine("Valor de la neurona {0}: " + rn.getOculta()[i], i + 1);
            }
        }
        public static void prtNeuSalida(RedNeuronal rn, int tamaño_salida){
            Console.WriteLine("########################## SALIDA ##########################");

            for (int i = 0; i < tamaño_salida; i++)
            {
                Console.WriteLine("Valor de la neurona {0}: " + rn.getSalida()[i], i + 1);
            }
        }
        public static void prtPesos1(RedNeuronal rn){
            Console.WriteLine("########################## PESOS 1 ##########################");

            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 35; j++)
                {
                    Console.WriteLine("Peso 1 " + j + " " + i + ": " + rn.getPesosE()[j,i]);
                }
            }
        }
        public static void prtPesos2(RedNeuronal rn){
            Console.WriteLine("########################## PESOS 2 ##########################");
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    Console.WriteLine("Peso 2 " + j + " " + i + ": " + rn.getPesosS()[j,i]);
                }
            }
        }

        public static int especular(double[] salida){
            
            double maxValue = salida.Max();
            int maxIndex = salida.ToList().IndexOf(maxValue);
            
            return maxIndex;
        }

    }
}