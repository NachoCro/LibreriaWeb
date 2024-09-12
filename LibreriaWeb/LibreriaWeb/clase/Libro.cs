using System.Text;

namespace LibreriaWeb.clase;

public class Libro
{
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public Categoria Categoria { get; set; }
    public decimal Precio { get; set; }
    public int StockLibro { get; set; }

    public Libro(string titulo, string autor, Categoria categoria, decimal precio, int stockLibro)
    {
        Titulo = titulo;
        Autor = autor;
        this.Categoria = categoria;
        Precio = precio;
        StockLibro = stockLibro;
    }

    public void MostrarLibro()
    {
        Console.WriteLine($"{Titulo}" +
                          $"\nPor {Autor}" +
                          $"\nCategoria: \n{Categoria.Nombre}" +
                          $"\n{Categoria.Descripcion}" +
                          $"\nPrecio: {Precio}" +
                          $"\nHay {StockLibro} libros");
    }

    public bool ControlStock(int RestoStock)
    {
        bool validarResta = false;
        if (RestoStock < StockLibro)
        {
            StockLibro -= RestoStock;
            validarResta = true;
        }
        else
        {
            Console.WriteLine("No podes restar mas del stock que tenemos");
            
        }

        return validarResta;
    }
}