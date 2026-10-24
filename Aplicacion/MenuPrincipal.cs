using VeterinariaV2.Utilidades;

namespace VeterinariaV2.Aplicacion;

public static class MenuPrincipal
{
    public static void Ejecutar()
    {
        int opcion;
        do
        {
            Console.Clear();
            Console.WriteLine("====================================================");
            Console.WriteLine("      SISTEMA DE CONTROL CLÍNICO VETERINARIO v2");
            Console.WriteLine("====================================================");
            Console.WriteLine("1. Propietarios");
            Console.WriteLine("2. Veterinarios");
            Console.WriteLine("3. Mascotas");
            Console.WriteLine("4. Citas");
            Console.WriteLine("5. Consultas médicas");
            Console.WriteLine("0. Salir");
            Console.WriteLine("====================================================");
            opcion = Consola.Entero("Seleccione una opción: ");

            switch (opcion)
            {
                case 1: MenuPropietarios.Ejecutar(); break;
                case 2: MenuVeterinarios.Ejecutar(); break;
                case 3: MenuMascotas.Ejecutar(); break;
                case 4: MenuCitas.Ejecutar(); break;
                case 5: MenuConsultas.Ejecutar(); break;
                case 0: Console.WriteLine("Sistema finalizado."); break;
            }
        } while (opcion != 0);
    }
}
