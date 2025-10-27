using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════╗");
        Console.WriteLine("║  SISTEMA DE GESTIÓN DE INVENTARIO     ║");
        Console.WriteLine("╚════════════════════════════════════════╝\n");

        // Cargar productos
        string rutaArchivo = "inventario.csv";
        List<Producto> productos = ConsultasLinq.CargarProductos(rutaArchivo);

        if (productos.Count == 0)
        {
            Console.WriteLine("No se pudieron cargar productos. Verifica que el archivo 'inventario.csv' esté en la carpeta del proyecto.");
            Console.WriteLine("Presiona cualquier tecla para salir...");
            Console.ReadKey();
            return;
        }

        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine("┌─────────────────────────────┐");
            Console.WriteLine("│      MENÚ PRINCIPAL         │");
            Console.WriteLine("├─────────────────────────────┤");
            Console.WriteLine("│ 1. Stock menor a 10         │");
            Console.WriteLine("│ 2. Ordenar por precio DESC  │");
            Console.WriteLine("│ 3. Valor total inventario   │");
            Console.WriteLine("│ 4. Agrupar por categoría    │");
            Console.WriteLine("│ 5. Exportar a archivo .txt  │");
            Console.WriteLine("│ 6. Salir                    │");
            Console.WriteLine("└─────────────────────────────┘");
            Console.Write("\n► Selecciona una opción: ");

            string opcion = Console.ReadLine();
            Console.WriteLine();

            switch (opcion)
            {
                case "1":
                    ConsultasLinq.ProductosStockBajo(productos);
                    break;
                case "2":
                    ConsultasLinq.ProductosPorPrecioDesc(productos);
                    break;
                case "3":
                    ConsultasLinq.ValorTotalInventario(productos);
                    break;
                case "4":
                    ConsultasLinq.ProductosPorCategoria(productos);
                    break;
                case "5":
                    ConsultasLinq.ExportarResultados(productos, "reporte_inventario.txt");
                    break;
                case "6":
                    continuar = false;
                    Console.WriteLine("¡Hasta luego!");
                    break;
                default:
                    Console.WriteLine(" Opción inválida. Intenta de nuevo.");
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