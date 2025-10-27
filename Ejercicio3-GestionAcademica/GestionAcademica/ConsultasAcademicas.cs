using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ConsultasAcademicas
{
    public static List<Estudiante> CargarEstudiantes(string rutaArchivo)
    {
        List<Estudiante> estudiantes = new List<Estudiante>();

        try
        {
            string[] lineas = File.ReadAllLines(rutaArchivo, System.Text.Encoding.UTF8);


            // La primera línea son los encabezados, empezamos desde la línea 1
            for (int i = 1; i < lineas.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lineas[i]))
                    continue;

                string[] datos = lineas[i].Split(',');

                // El formato real es: Curso,Carnet,Nombre,Apellido,Nota
                if (datos.Length >= 5)
                {
                    try
                    {
                        string curso = datos[0].Trim();
                        string carnet = datos[1].Trim();
                        string nombre = datos[2].Trim();
                        string apellido = datos[3].Trim();
                        double nota = double.Parse(datos[4].Trim());

                        // Edad por defecto 20 (ya que no viene en el CSV)
                        int edad = 20;

                        estudiantes.Add(new Estudiante(nombre, apellido, edad, carnet, curso, nota));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"DEBUG: Error en línea {i}: {ex.Message}");
                    }
                }
            }

            Console.WriteLine($"✓ Se cargaron {estudiantes.Count} estudiantes correctamente.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error al cargar el archivo: {ex.Message}");
        }

        return estudiantes;
    }

    // 5a. Estudiantes aprobados (nota >= 70)
    public static void EstudiantesAprobados(List<Estudiante> estudiantes)
    {
        Console.WriteLine("=== ESTUDIANTES APROBADOS (Nota >= 70) ===");

        var aprobados = estudiantes
            .Where(e => e.EstaAprobado())
            .OrderByDescending(e => e.Nota);

        foreach (var estudiante in aprobados)
        {
            estudiante.MostrarInformacion();
        }

        Console.WriteLine($"\nTotal de estudiantes aprobados: {aprobados.Count()}\n");
    }

    // 5b. Top 5 estudiantes por curso
    public static void Top5PorCurso(List<Estudiante> estudiantes)
    {
        Console.WriteLine("=== TOP 5 ESTUDIANTES POR CURSO ===");

        var cursos = estudiantes
            .Select(e => e.Curso)
            .Distinct()
            .OrderBy(c => c);

        foreach (var curso in cursos)
        {
            Console.WriteLine($"\n--- Curso: {curso} ---");

            var top5 = estudiantes
                .Where(e => e.Curso == curso)
                .OrderByDescending(e => e.Nota)
                .Take(5);

            int posicion = 1;
            foreach (var estudiante in top5)
            {
                Console.WriteLine($"{posicion}. {estudiante}");
                posicion++;
            }
        }
        Console.WriteLine();
    }

    // 5c. Calcular promedio por curso
    public static void PromedioPorCurso(List<Estudiante> estudiantes)
    {
        Console.WriteLine("=== PROMEDIO POR CURSO ===");

        var promedios = estudiantes
            .GroupBy(e => e.Curso)
            .Select(g => new
            {
                Curso = g.Key,
                Promedio = g.Average(e => e.Nota),
                Cantidad = g.Count()
            })
            .OrderByDescending(x => x.Promedio);

        foreach (var item in promedios)
        {
            Console.WriteLine($"Curso: {item.Curso}");
            Console.WriteLine($"  Promedio: {item.Promedio:F2}");
            Console.WriteLine($"  Estudiantes: {item.Cantidad}");
            Console.WriteLine();
        }
    }

    // 5d. Top 10 estudiantes de todos los cursos
    public static void Top10General(List<Estudiante> estudiantes)
    {
        Console.WriteLine("=== TOP 10 ESTUDIANTES (TODOS LOS CURSOS) ===");

        var top10 = estudiantes
            .OrderByDescending(e => e.Nota)
            .ThenBy(e => e.Apellido)
            .Take(10);

        int posicion = 1;
        foreach (var estudiante in top10)
        {
            Console.WriteLine($"{posicion}. {estudiante}");
            posicion++;
        }
        Console.WriteLine();
    }

    // 5e. Ranking de estudiantes
    public static void RankingEstudiantes(List<Estudiante> estudiantes)
    {
        Console.WriteLine("=== RANKING GENERAL DE ESTUDIANTES ===");

        var ranking = estudiantes
            .OrderByDescending(e => e.Nota)
            .ThenBy(e => e.Apellido)
            .Select((estudiante, index) => new
            {
                Posicion = index + 1,
                Estudiante = estudiante
            });

        foreach (var item in ranking)
        {
            Console.WriteLine($"#{item.Posicion} - {item.Estudiante}");
        }
        Console.WriteLine();
    }

    // 5f. Mejor estudiante por curso
    public static void MejorEstudiantePorCurso(List<Estudiante> estudiantes)
    {
        Console.WriteLine("=== MEJOR ESTUDIANTE POR CURSO ===");

        var mejores = estudiantes
            .GroupBy(e => e.Curso)
            .Select(g => g.OrderByDescending(e => e.Nota).First())
            .OrderBy(e => e.Curso);

        foreach (var estudiante in mejores)
        {
            Console.WriteLine($"Curso: {estudiante.Curso}");
            Console.WriteLine($"  {estudiante}");
            Console.WriteLine();
        }
    }

    // 5g. Estudiantes por intervalos de notas
    public static void EstudiantesPorIntervalos(List<Estudiante> estudiantes)
    {
        Console.WriteLine("=== ESTUDIANTES POR INTERVALOS DE NOTA ===");

        var intervalo1 = estudiantes.Where(e => e.Nota >= 0 && e.Nota < 60);
        var intervalo2 = estudiantes.Where(e => e.Nota >= 60 && e.Nota < 80);
        var intervalo3 = estudiantes.Where(e => e.Nota >= 80 && e.Nota <= 100);

        Console.WriteLine($"\n--- Rango 0-59 (Reprobados) ---");
        Console.WriteLine($"Cantidad: {intervalo1.Count()}");
        foreach (var e in intervalo1.OrderBy(e => e.Nota))
        {
            Console.WriteLine($"  {e}");
        }

        Console.WriteLine($"\n--- Rango 60-79 (Aprobados) ---");
        Console.WriteLine($"Cantidad: {intervalo2.Count()}");
        foreach (var e in intervalo2.OrderBy(e => e.Nota))
        {
            Console.WriteLine($"  {e}");
        }

        Console.WriteLine($"\n--- Rango 80-100 (Excelentes) ---");
        Console.WriteLine($"Cantidad: {intervalo3.Count()}");
        foreach (var e in intervalo3.OrderBy(e => e.Nota))
        {
            Console.WriteLine($"  {e}");
        }
        Console.WriteLine();
    }

    // Exportar resultados
    public static void ExportarResultados(List<Estudiante> estudiantes, string rutaSalida)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(rutaSalida))
            {
                writer.WriteLine("========================================");
                writer.WriteLine("REPORTE ACADÉMICO");
                writer.WriteLine($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                writer.WriteLine("========================================\n");

                // Estudiantes aprobados
                writer.WriteLine("1. ESTUDIANTES APROBADOS");
                writer.WriteLine("------------------------------------");
                var aprobados = estudiantes.Where(e => e.EstaAprobado()).OrderByDescending(e => e.Nota);
                foreach (var e in aprobados)
                {
                    writer.WriteLine(e);
                }
                writer.WriteLine($"\nTotal: {aprobados.Count()} estudiantes\n");

                // Promedio por curso
                writer.WriteLine("2. PROMEDIO POR CURSO");
                writer.WriteLine("------------------------------------");
                var promedios = estudiantes.GroupBy(e => e.Curso);
                foreach (var grupo in promedios)
                {
                    writer.WriteLine($"Curso: {grupo.Key}");
                    writer.WriteLine($"Promedio: {grupo.Average(e => e.Nota):F2}");
                    writer.WriteLine($"Estudiantes: {grupo.Count()}\n");
                }

                // Top 10 general
                writer.WriteLine("3. TOP 10 ESTUDIANTES");
                writer.WriteLine("------------------------------------");
                var top10 = estudiantes.OrderByDescending(e => e.Nota).Take(10);
                int pos = 1;
                foreach (var e in top10)
                {
                    writer.WriteLine($"{pos}. {e}");
                    pos++;
                }
            }

            Console.WriteLine($"✓ Resultados exportados exitosamente a: {rutaSalida}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error al exportar: {ex.Message}");
        }
    }
}