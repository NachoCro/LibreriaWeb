using System;
using System.Collections.Generic;
using System.Linq;

// --- Clases ---

public class Cliente
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string CorreoElectronico { get; set; }
    public string Contraseña { get; set; }

    public bool VerificarContraseña(string contraseña)
    {
        return Contraseña == contraseña;
    }
}

public class Libro
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public decimal Precio { get; set; }
    public int Stock { get; set; }

    public bool VerificarStock(int cantidad)
    {
        return Stock >= cantidad;
    }

    public void DescontarStock(int cantidad)
    {
        Stock -= cantidad;
    }
}

public class Rubro
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public List<Libro> Libros { get; set; } = new List<Libro>();

    public List<Libro> ObtenerCatalogo()
    {
        return Libros;
    }
}

public class Pedido
{
    public Cliente Cliente { get; set; }
    public List<(Libro, int)> Libros { get; set; } = new List<(Libro, int)>();

    public void AgregarLibro(Libro libro, int cantidad)
    {
        Libros.Add((libro, cantidad));
    }

    public decimal CalcularTotal()
    {
        return Libros.Sum(l => l.Item1.Precio * l.Item2);
    }
}

public class Compra
{
    public Pedido Pedido { get; set; }
    public decimal Total { get; set; }
    public Pago Pago { get; set; }

    public Compra(Pedido pedido)
    {
        Pedido = pedido;
        Total = pedido.CalcularTotal();
    }

    public bool ConfirmarCompra()
    {
        foreach (var (libro, cantidad) in Pedido.Libros)
        {
            if (!libro.VerificarStock(cantidad))
            {
                Console.WriteLine($"No hay suficiente stock para el libro: {libro.Titulo}");
                return false;
            }
        }

        foreach (var (libro, cantidad) in Pedido.Libros)
        {
            libro.DescontarStock(cantidad);
        }
        return true;
    }
}

public class Pago
{
    public string NumeroTarjeta { get; set; }
    public string FechaVencimiento { get; set; }
    public string CodigoSeguridad { get; set; }

    public bool VerificarTarjeta()
    {
        return !string.IsNullOrEmpty(NumeroTarjeta) && !string.IsNullOrEmpty(FechaVencimiento) && !string.IsNullOrEmpty(CodigoSeguridad);
    }
}

// --- Simulación del Sistema ---

class Program
{
    static List<Cliente> clientes = new List<Cliente>();
    static List<Rubro> rubros = new List<Rubro>();

    static void Main(string[] args)
    {
        // Inicialización de datos
        InicializarDatos();

        // Logueo del cliente
        Cliente cliente = LoguearCliente();
        if (cliente == null)
        {
            Console.WriteLine("Credenciales incorrectas. Fin del programa.");
            return;
        }

        // Selección de rubro
        Rubro rubroSeleccionado = SeleccionarRubro();

        // Selección de libros y creación del pedido
        Pedido pedido = new Pedido { Cliente = cliente };
        SeleccionarLibros(rubroSeleccionado, pedido);

        // Confirmación de la compra
        if (ConfirmarCompra(pedido))
        {
            // Procesar pago
            Pago pago = SolicitarDatosPago();
            if (pago.VerificarTarjeta())
            {
                Compra compra = new Compra(pedido) { Pago = pago };
                if (compra.ConfirmarCompra())
                {
                    Console.WriteLine($"Compra realizada con éxito. Total: {compra.Total:C}");
                }
            }
            else
            {
                Console.WriteLine("Datos de la tarjeta incorrectos. Compra cancelada.");
            }
        }
        else
        {
            Console.WriteLine("Compra cancelada por falta de stock.");
        }
    }

    static void InicializarDatos()
    {
        clientes.Add(new Cliente { Id = 1, Nombre = "Juan Perez", CorreoElectronico = "juan@mail.com", Contraseña = "1234" });

        // Rubros y libros
        Rubro rubroFiccion = new Rubro { Id = 1, Nombre = "Ficción" };
        rubroFiccion.Libros.Add(new Libro { Id = 1, Titulo = "El señor de los anillos", Autor = "J.R.R. Tolkien", Precio = 500.00M, Stock = 5 });
        rubroFiccion.Libros.Add(new Libro { Id = 2, Titulo = "1984", Autor = "George Orwell", Precio = 300.00M, Stock = 2 });

        Rubro rubroCiencia = new Rubro { Id = 2, Nombre = "Ciencia" };
        rubroCiencia.Libros.Add(new Libro { Id = 3, Titulo = "Breve historia del tiempo", Autor = "Stephen Hawking", Precio = 400.00M, Stock = 3 });

        rubros.Add(rubroFiccion);
        rubros.Add(rubroCiencia);
    }

    static Cliente LoguearCliente()
    {
        Console.WriteLine("Ingrese su correo electrónico:");
        string correo = Console.ReadLine();
        Console.WriteLine("Ingrese su contraseña:");
        string contraseña = Console.ReadLine();

        return clientes.FirstOrDefault(c => c.CorreoElectronico == correo && c.VerificarContraseña(contraseña));
    }

    static Rubro SeleccionarRubro()
    {
        Console.WriteLine("Seleccione un rubro:");
        for (int i = 0; i < rubros.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {rubros[i].Nombre}");
        }

        int opcion = int.Parse(Console.ReadLine());
        return rubros[opcion - 1];
    }

    static void SeleccionarLibros(Rubro rubro, Pedido pedido)
    {
        bool continuar = true;
        while (continuar)
        {
            Console.WriteLine("Seleccione un libro:");
            for (int i = 0; i < rubro.Libros.Count; i++)
            {
                var libro = rubro.Libros[i];
                Console.WriteLine($"{i + 1}. {libro.Titulo} - {libro.Autor} - {libro.Precio:C} (Stock: {libro.Stock})");
            }

            int opcion = int.Parse(Console.ReadLine());
            Libro libroSeleccionado = rubro.Libros[opcion - 1];

            Console.WriteLine("Ingrese la cantidad a comprar:");
            int cantidad = int.Parse(Console.ReadLine());

            pedido.AgregarLibro(libroSeleccionado, cantidad);

            Console.WriteLine("¿Desea agregar otro libro? (s/n)");
            continuar = Console.ReadLine().ToLower() == "s";
        }
    }

    static bool ConfirmarCompra(Pedido pedido)
    {
        Console.WriteLine("¿Desea confirmar la compra? (s/n)");
        return Console.ReadLine().ToLower() == "s";
    }

    static Pago SolicitarDatosPago()
    {
        Console.WriteLine("Ingrese los datos de la tarjeta de crédito:");
        Console.WriteLine("Número de tarjeta:");
        string numero = Console.ReadLine();
        Console.WriteLine("Fecha de vencimiento:");
        string vencimiento = Console.ReadLine();
        Console.WriteLine("Código de seguridad:");
        string codigo = Console.ReadLine();

        return new Pago { NumeroTarjeta = numero, FechaVencimiento = vencimiento, CodigoSeguridad = codigo };
    }
}
