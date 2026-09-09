using System.Diagnostics;
using System.Reflection;

internal static class Program
{
    private const string AsarRelative = @"resources\app.asar";

    private static readonly string[] DefaultInstallDirs =
    [
        @"C:\Program Files\TruckNav",
        @"C:\Program Files (x86)\TruckNav",
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            @"Programs\TruckNav"),
    ];

    [STAThread]
    private static int Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);

        try
        {
            var installDir = FindInstallDir();
            if (installDir is null)
            {
                var ask = MessageBox.Show(
                    "No se ha encontrado TruckNav en las rutas habituales.\n\n" +
                    "¿Quieres indicar manualmente dónde está instalado?\n" +
                    "(Selecciona el archivo TruckNav.exe)",
                    "TruckNav Español",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (ask != DialogResult.Yes)
                {
                    return 0;
                }

                installDir = PickInstallDirManually();
                if (installDir is null)
                {
                    return 0;
                }
            }

            var asarPath = GetAsarPath(installDir);
            if (asarPath is null)
            {
                MessageBox.Show(
                    "Esa carpeta no parece una instalación de TruckNav.\n\n" +
                    "Debe contener TruckNav.exe y el archivo:\nresources\\app.asar",
                    "TruckNav Español",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return 1;
            }

            var confirm = MessageBox.Show(
                "Se va a instalar el español en:\n" + installDir + "\n\n" +
                "• Se cerrará TruckNav si está abierto.\n" +
                "• Se guardará una copia de seguridad (app.asar.bak).\n" +
                "• El idioma por defecto pasará a ser español, incluidas las indicaciones de salida.\n\n" +
                "¿Continuar?",
                "TruckNav Español",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirm != DialogResult.Yes)
            {
                return 0;
            }

            CloseTruckNav();

            var backupPath = asarPath + ".bak";
            if (!File.Exists(backupPath))
            {
                File.Copy(asarPath, backupPath, overwrite: false);
            }

            using var source = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("app.asar");
            if (source is null)
            {
                throw new InvalidOperationException(
                    "No se encontró el paquete de traducción dentro del instalador.");
            }

            using (var target = File.Create(asarPath))
            {
                source.CopyTo(target);
            }

            MessageBox.Show(
                "Español instalado correctamente.\n\n" +
                "Abre TruckNav. Si sigue en inglés, ve a Ajustes → Idioma y elige Español.\n\n" +
                "Copia de seguridad:\n" + backupPath,
                "TruckNav Español",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            return 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                "No se pudo instalar el español:\n\n" + ex.Message,
                "TruckNav Español",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            return 1;
        }
    }

    private static string? FindInstallDir()
    {
        foreach (var candidate in DefaultInstallDirs)
        {
            if (GetAsarPath(candidate) is not null)
            {
                return candidate;
            }
        }

        foreach (var process in Process.GetProcessesByName("TruckNav"))
        {
            try
            {
                var exePath = process.MainModule?.FileName;
                if (string.IsNullOrWhiteSpace(exePath))
                {
                    continue;
                }

                var dir = Path.GetDirectoryName(exePath);
                if (dir is not null && GetAsarPath(dir) is not null)
                {
                    return dir;
                }
            }
            catch
            {
                // Accessing MainModule can fail for some processes.
            }
            finally
            {
                process.Dispose();
            }
        }

        return null;
    }

    private static string? PickInstallDirManually()
    {
        using var dialog = new OpenFileDialog
        {
            Title = "Selecciona TruckNav.exe",
            Filter = "TruckNav.exe|TruckNav.exe|Ejecutables (*.exe)|*.exe|Todos los archivos (*.*)|*.*",
            FileName = "TruckNav.exe",
            CheckFileExists = true,
            Multiselect = false,
        };

        if (dialog.ShowDialog() != DialogResult.OK)
        {
            return null;
        }

        return Path.GetDirectoryName(dialog.FileName);
    }

    private static string? GetAsarPath(string? installDir)
    {
        if (string.IsNullOrWhiteSpace(installDir) || !Directory.Exists(installDir))
        {
            return null;
        }

        var asarPath = Path.Combine(installDir, AsarRelative);
        if (File.Exists(asarPath) && File.Exists(Path.Combine(installDir, "TruckNav.exe")))
        {
            return asarPath;
        }

        return null;
    }

    private static void CloseTruckNav()
    {
        var processes = Process.GetProcessesByName("TruckNav");
        foreach (var process in processes)
        {
            try
            {
                if (!process.HasExited)
                {
                    process.CloseMainWindow();
                    if (!process.WaitForExit(4000))
                    {
                        process.Kill(entireProcessTree: true);
                        process.WaitForExit(4000);
                    }
                }
            }
            catch
            {
                // Follow-up copy will fail clearly if the file is still locked.
            }
            finally
            {
                process.Dispose();
            }
        }
    }
}
