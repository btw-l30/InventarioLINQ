using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║  SISTEMA DE GESTIÓN ACADÉMICA         ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        // Cargar estudiantes
        string rutaArchivo = "estudiantes.csv";
        List<Estudiante> estudiantes = ConsultasAcademicas.CargarEstudiantes(rutaArchivo);

        if (estudiantes.Count == 0)
        {
            Console.WriteLine("No se pudieron cargar estudiantes. Verifica que el archivo 'estudiantes.csv' esté en la carpeta del proyecto.");
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
            return;
        }

        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine("┌──────────────────────────────────────┐");
            Console.WriteLine("│         MENÚ PRINCIPAL               │");
            Console.WriteLine("├──────────────────────────────────────┤");
            Console.WriteLine("│ 1. Estudiantes aprobados (>= 70)     │");
            Console.WriteLine("│ 2. Top 5 por curso                   │");
            Console.WriteLine("│ 3. Promedio por curso                │");
            Console.WriteLine("│ 4. Top 10 general                    │");
            Console.WriteLine("│ 5. Ranking de estudiantes            │");
            Console.WriteLine("│ 6. Mejor estudiante por curso        │");
            Console.WriteLine("│ 7. Estudiantes por intervalos        │");
            Console.WriteLine("│ 8. Exportar a archivo .txt           │");
            Console.WriteLine("│ 9. Salir                             │");
            Console.WriteLine("└──────────────────────────────────────┘");
            Console.Write("\n► Selecciona una opción: ");

            string opcion = Console.ReadLine();
            Console.WriteLine();

            switch (opcion)
            {
                case "1":
                    ConsultasAcademicas.EstudiantesAprobados(estudiantes);
                    break;
                case "2":
                    ConsultasAcademicas.Top5PorCurso(estudiantes);
                    break;
                case "3":
                    ConsultasAcademicas.PromedioPorCurso(estudiantes);
                    break;
                case "4":
                    ConsultasAcademicas.Top10General(estudiantes);
                    break;
                case "5":
                    ConsultasAcademicas.RankingEstudiantes(estudiantes);
                    break;
                case "6":
                    ConsultasAcademicas.MejorEstudiantePorCurso(estudiantes);
                    break;
                case "7":
                    ConsultasAcademicas.EstudiantesPorIntervalos(estudiantes);
                    break;
                case "8":
                    ConsultasAcademicas.ExportarResultados(estudiantes, "reporte_academico.txt");
                    break;
                case "9":
                    continuar = false;
                    Console.WriteLine("¡Hasta luego! 👋");
                    break;
                default:
                    Console.WriteLine("⚠ Opción inválida. Intenta de nuevo.");
                    break;
            }

            if (continuar)
            {
                Console.WriteLine("\nPresiona cualquier tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}