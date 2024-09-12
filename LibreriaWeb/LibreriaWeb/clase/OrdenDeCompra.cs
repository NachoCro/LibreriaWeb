namespace LibreriaWeb.clase;

public class OrdenDeCompra
{
    public Cliente Cliente { get; set; }
    public int StockOC { get; set; }
    public List<Libro> Libros { get; set; }

    public OrdenDeCompra(Cliente cliente, int stockOc, List<Libro> libros)
    {
        Cliente = cliente;
        StockOC = stockOc;
        Libros = libros;
    }

    public decimal CalcularTotal()
    {
        decimal total = 0;
        foreach (Libro libro in Libros)
        {
            total += libro.Precio;
        }

        return total;

    }

    public void MostrarDetalle()
    {
        Console.WriteLine("Tenes todos estos libros: \n");
        foreach (Libro libro in Libros)
        {
            libro.MostrarLibro();
        }
        Console.WriteLine($"Total: {CalcularTotal()}");
    }

    public void AniadirLibroOC()
    {
        Console.WriteLine("Ingrese nombre de libro");
        string TituloLibro = Console.ReadLine().Trim().ToLower();
        bool Ingreso = false
        if (!string.IsNullOrEmpty(TituloLibro))
        {
            foreach (Libro libro in Libros)
            {
                if (TituloLibro == libro.Titulo.Trim().ToLower())
                {
                    
                    Libro 
                    Ingreso = true;
                }
            }
        }
    }
}