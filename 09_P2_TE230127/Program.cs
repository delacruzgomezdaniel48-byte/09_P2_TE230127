namespace EvaluacionBombasElectricas
{
    // ==========================================
    // CLASE: BombaElectrica
    // Representa la entidad de una bomba hidráulica/eléctrica
    // ==========================================
    public class BombaElectrica
    {
        // PROPIEDADES (con sus respectivos tipos de datos)
        public string Identificador { get; set; }
        public double Voltaje { get; set; }     // Medido en Volts (V)
        public double Corriente { get; set; }   // Medida en Amperios (A)
        public double Caudal { get; set; }      // Medido en Litros por minuto (L/min)

        // Constructor
        public BombaElectrica(string identificador, double voltaje, double corriente, double caudal)
        {
            Identificador = identificador;
            Voltaje = voltaje;
            Corriente = corriente;
            Caudal = caudal;
        }

        // MÉTODOS DE LA CLASE

        /// <summary>
        /// Calcula la potencia eléctrica consumida por la bomba: P = V * I
        /// </summary>
        /// <returns>Potencia en Watts (W)</returns>
        public double CalcularPotencia()
        {
            return Voltaje * Corriente;
        }

        /// <summary>
        /// Calcula el indicador de desempeño didáctico: Caudal / Potencia
        /// </summary>
        /// <returns>Indicador en (L/min)/W</returns>
        public double CalcularIndicadorDesempeno()
        {
            double potencia = CalcularPotencia();

            // VALIDACIÓN: Evitar división entre cero o valores inconsistentes
            if (potencia <= 0)
            {
                return 0.0;
            }

            return Caudal / potencia;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Evaluación y Comparación de Bombas Eléctricas";
            Console.WriteLine("=================================================");
            Console.WriteLine("   SISTEMA DE EVALUACIÓN DE BOMBAS ELÉCTRICAS   ");
            Console.WriteLine("=================================================\n");

            // CREACIÓN Y CAPTURA DE OBJETOS DE LA CLASE BombaElectrica
            Console.WriteLine("--- DATOS DE LA BOMBA 1 ---");
            BombaElectrica bomba1 = CapturarDatosBomba("1");

            Console.WriteLine("\n--- DATOS DE LA BOMBA 2 ---");
            BombaElectrica bomba2 = CapturarDatosBomba("2");

            // CÁLCULO DE RESULTADOS
            double potencia1 = bomba1.CalcularPotencia();
            double indicador1 = bomba1.CalcularIndicadorDesempeno();

            double potencia2 = bomba2.CalcularPotencia();
            double indicador2 = bomba2.CalcularIndicadorDesempeno();

            // IMPRESIÓN DE RESULTADOS CON UNIDADES DE MEDIDA
            Console.WriteLine("\n=================================================");
            Console.WriteLine("              RESULTADOS DE EVALUACIÓN           ");
            Console.WriteLine("=================================================");
            Console.WriteLine($"Bomba 1 [{bomba1.Identificador}]:");
            Console.WriteLine($"  - Voltaje:               {bomba1.Voltaje:F2} V");
            Console.WriteLine($"  - Corriente:             {bomba1.Corriente:F2} A");
            Console.WriteLine($"  - Caudal:                {bomba1.Caudal:F2} L/min");
            Console.WriteLine($"  - Potencia Eléctrica:    {potencia1:F2} W");
            Console.WriteLine($"  - Indicador Desempeño:   {indicador1:F4} (L/min)/W");

            Console.WriteLine("\n-------------------------------------------------");
            Console.WriteLine($"Bomba 2 [{bomba2.Identificador}]:");
            Console.WriteLine($"  - Voltaje:               {bomba2.Voltaje:F2} V");
            Console.WriteLine($"  - Corriente:             {bomba2.Corriente:F2} A");
            Console.WriteLine($"  - Caudal:                {bomba2.Caudal:F2} L/min");
            Console.WriteLine($"  - Potencia Eléctrica:    {potencia2:F2} W");
            Console.WriteLine($"  - Indicador Desempeño:   {indicador2:F4} (L/min)/W");

            // COMPARACIÓN DE DESEMPEÑO
            Console.WriteLine("\n=================================================");
            Console.WriteLine("             COMPARACIÓN Y CONCLUSIÓN            ");
            Console.WriteLine("=================================================");

            if (Math.Abs(indicador1 - indicador2) < 1e-7)
            {
                Console.WriteLine("RESULTADO: Existe un EMPATE. Ambas bombas entregan la misma cantidad de caudal por watt.");
            }
            else if (indicador1 > indicador2)
            {
                Console.WriteLine($"RESULTADO: La bomba [{bomba1.Identificador}] es MÁS EFICIENTE.");
                Console.WriteLine($"Entrega mayor caudal por watt ({indicador1:F4} vs {indicador2:F4} (L/min)/W).");
            }
            else
            {
                Console.WriteLine($"RESULTADO: La bomba [{bomba2.Identificador}] es MÁS EFICIENTE.");
                Console.WriteLine($"Entrega mayor caudal por watt ({indicador2:F4} vs {indicador1:F4} (L/min)/W).");
            }

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }

        /// <summary>
        /// Método auxiliar para capturar y validar las entradas numéricas del usuario,
        /// garantizando la prevención de valores negativos, cero o entradas no válidas.
        /// </summary>
        static BombaElectrica CapturarDatosBomba(string numeroBomba)
        {
            Console.Write($"Ingrese el identificador/nombre de la Bomba {numeroBomba}: ");
            string id = Console.ReadLine();

            double voltaje = LeerDoublePositivo("Ingrese el voltaje (V): ");
            double corriente = LeerDoublePositivo("Ingrese la corriente (A): ");
            double caudal = LeerDoublePositivo("Ingrese el caudal (L/min): ");

            // Creación e instanciación del OBJETO
            return new BombaElectrica(id, voltaje, corriente, caudal);
        }

        /// <summary>
        /// Valida la lectura de un número real garantizando que sea estrictamente mayor a cero (> 0)
        /// evitando divisiones entre cero o valores físicos incoherentes.
        /// </summary>
        static double LeerDoublePositivo(string mensaje)
        {
            double valor;
            while (true)
            {
                Console.Write(mensaje);
                string entrada = Console.ReadLine();

                if (double.TryParse(entrada, out valor) && valor > 0)
                {
                    break;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("  [ERROR] Entrada inválida. Debe ingresar un valor numérico positivo mayor a cero.");
                Console.ResetColor();
            }
            return valor;
        }
    }
}
