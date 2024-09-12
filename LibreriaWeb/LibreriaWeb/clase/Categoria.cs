namespace LibreriaWeb.clase;

public class Categoria
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }

    public Categoria(string nombre, string descripcion)
    {
        Nombre = nombre;
        Descripcion = descripcion;
    }
    
}