using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class ConsultasLinq
{
    public static List<Producto> CargarProductos(string rutaArchivo)
    {
        List<Producto> productos = new List<Producto>();

        try
        {
            string[] lineas = File.ReadAllLines(rutaArchivo);

            for (int i = 1; i < lineas.Length; i++)
            {
                string[] datos = lineas[i].Split(',');

                if (datos.Length >= 5)
                {
                    int id = int.Parse(datos[0]);
                    string nombre = datos[1];
                    string categoria = datos[2];
                    decimal precio = decimal.Parse(datos[3]);
                    int stock = int.Parse(datos[4]);

                    productos.Add(new Producto(id, nombre, categoria, precio, stock));
                }
            }

            Console.WriteLine($"✓ Se cargaron {productos.Count} productos correctamente.\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"✗ Error al cargar el archivo: {ex.Message}");
        }

        return productos;
    }

    public static void ProductosStockBajo(List<Producto> productos)
    {
        Console.WriteLine("=== PRODUCTOS CON STOCK MENOR A 10 ===");

        var productosStockBajo = productos
            .Where(p => p.Stock < 10)
            .OrderBy(p => p.Stock);

        foreach (var producto in productosStockBajo)
        {
            Console.WriteLine(producto);
        }

        Console.WriteLine($"\nTotal de productos con stock bajo: {productosStockBajo.Count()}\n");
    }

    public static void ProductosPorPrecioDesc(List<Producto> productos)
    {
        Console.WriteLine("=== PRODUCTOS ORDENADOS POR PRECIO (DESCENDENTE) ===");

        var productosOrdenados = productos
            .OrderByDescending(p => p.Precio);

        foreach (var producto in productosOrdenados)
        {
            Console.WriteLine(producto);
        }
        Console.WriteLine();
    }

    public static void ValorTotalInventario(List<Producto> productos)
    {
        Console.WriteLine("=== VALOR TOTAL DEL INVENTARIO ===");

        decimal valorTotal = productos
            .Sum(p => p.Precio * p.Stock);

        Console.WriteLine($"Valor total del inventario: ${valorTotal:F2}\n");
    }

    public static void ProductosPorCategoria(List<Producto> productos)
    {
        Console.WriteLine("=== PRODUCTOS AGRUPADOS POR CATEGORÍA ===");

        var productosPorCategoria = productos
            .GroupBy(p => p.Categoria)
            .OrderBy(g => g.Key);

        foreach (var grupo in productosPorCategoria)
        {
            Console.WriteLine($"\n--- Categoría: {grupo.Key} ---");
            Console.WriteLine($"Cantidad de productos: {grupo.Count()}");
            Console.WriteLine($"Valor total: ${grupo.Sum(p => p.Precio * p.Stock):F2}");
            Console.WriteLine("Productos:");

            foreach (var producto in grupo)
            {
                Console.WriteLine($"  - {producto}");
            }
        }
        Console.WriteLine();
    }

    public static void ExportarResultados(List<Producto> productos, string rutaSalida)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(rutaSalida))
            {
                writer.WriteLine("========================================");
                writer.WriteLine("REPORTE DE INVENTARIO");
                writer.WriteLine($"Fecha: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                writer.WriteLine("========================================\n");

                writer.WriteLine("1. PRODUCTOS CON STOCK MENOR A 10");
                writer.WriteLine("------------------------------------");
                var stockBajo = productos.Where(p => p.Stock < 10).OrderBy(p => p.Stock);
                foreach (var p in stockBajo)
                {
                    writer.WriteLine(p);
                }
                writer.WriteLine($"\nTotal: {stockBajo.Count()} productos\n");

                writer.WriteLine("2. PRODUCTOS ORDENADOS POR PRECIO (DESC)");
                writer.WriteLine("------------------------------------");
                foreach (var p in productos.OrderByDescending(p => p.Precio))
                {
                    writer.WriteLine(p);
                }
                writer.WriteLine();

                writer.WriteLine("3. VALOR TOTAL DEL INVENTARIO");
                writer.WriteLine("------------------------------------");
                decimal valorTotal = productos.Sum(p => p.Precio * p.Stock);
                writer.WriteLine($"Valor total: ${valorTotal:F2}\n");

                writer.WriteLine("4. PRODUCTOS AGRUPADOS POR CATEGORÍA");
                writer.WriteLine("------------------------------------");
                var porCategoria = productos.GroupBy(p => p.Categoria).OrderBy(g => g.Key);
                foreach (var grupo in porCategoria)
                {
                    writer.WriteLine($"\nCategoría: {grupo.Key}");
                    writer.WriteLine($"Cantidad: {grupo.Count()}");
                    writer.WriteLine($"Valor total: ${grupo.Sum(p => p.Precio * p.Stock):F2}");
                    foreach (var p in grupo)
                    {
                        writer.WriteLine($"  - {p}");
                    }
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