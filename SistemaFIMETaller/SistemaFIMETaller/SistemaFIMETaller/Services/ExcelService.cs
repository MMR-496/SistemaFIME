using ClosedXML.Excel;
using SistemaFIMETaller.Models;

public class ExcelService
{
    public byte[] GenerarExcelPrestamos(List<Prestamo> prestamos)
    {
        using var wb = new XLWorkbook();
        var ws = wb.AddWorksheet("Préstamos");

        
        ws.Cell(1, 1).Value = "ID";
        ws.Cell(1, 2).Value = "Alumno";
        ws.Cell(1, 3).Value = "Fecha";
        ws.Cell(1, 4).Value = "Materiales";
        ws.Cell(1, 5).Value = "Estado";
        ws.Cell(1, 6).Value = "Observaciones";

        int row = 2;

        foreach (var p in prestamos)
        {
            ws.Cell(row, 1).Value = p.Id;
            ws.Cell(row, 2).Value = p.Alumno?.Nombre;
            ws.Cell(row, 3).Value = p.Fecha.ToString("dd/MM/yyyy");

            var mats = p.PrestamoMateriales?.Any() == true
                ? string.Join(", ", p.PrestamoMateriales.Select(m => m.Material?.Nombre))
                : "(sin materiales)";

            ws.Cell(row, 4).Value = mats;
            ws.Cell(row, 5).Value = p.Estado ? "Activo" : "Entregado";
            ws.Cell(row, 6).Value = string.IsNullOrWhiteSpace(p.Observaciones) ? "-" : p.Observaciones;

            row++;
        }

        using var stream = new MemoryStream();
        wb.SaveAs(stream);
        return stream.ToArray();
    }

    public byte[] GenerarExcelAlumnos(List<Alumno> alumnos)
    {
        using var wb = new XLWorkbook();
        var ws = wb.AddWorksheet("Alumnos");

        
        ws.Cell(1, 1).Value = "Nombre";
        ws.Cell(1, 2).Value = "Apellido";
        ws.Cell(1, 3).Value = "Matrícula";
        ws.Cell(1, 4).Value = "Grupo";

        ws.Row(1).Style.Font.Bold = true;

        int row = 2;

        foreach (var a in alumnos)
        {
            ws.Cell(row, 1).Value = a.Nombre;
            ws.Cell(row, 2).Value = a.Apellido;
            ws.Cell(row, 3).Value = a.NumeroCuenta;
            ws.Cell(row, 4).Value = a.Grupo;
            row++;
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        wb.SaveAs(stream);
        return stream.ToArray();
    }

    public byte[] GenerarExcelProfesores(List<Profesor> profesores)
    {
        using var wb = new XLWorkbook();
        var ws = wb.AddWorksheet("Profesores");


        ws.Cell(1, 1).Value = "Nombre";
        ws.Cell(1, 2).Value = "Apellido";
        ws.Cell(1, 3).Value = "Número de Cuenta";

        ws.Row(1).Style.Font.Bold = true;

        int row = 2;

        foreach (var a in profesores)
        {
            ws.Cell(row, 1).Value = a.NombreProfesor;
            ws.Cell(row, 2).Value = a.ApellidoProfesor;
            ws.Cell(row, 3).Value = a.NumeroCuentaProfesor;
            row++;
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        wb.SaveAs(stream);
        return stream.ToArray();
    }

    public byte[] GenerarExcelMateriales(List<Material> materiales)
    {
        using var wb = new XLWorkbook();
        var ws = wb.AddWorksheet("Materiales");

        ws.Cell(1, 1).Value = "Número";
        ws.Cell(1, 2).Value = "Nombre";
        ws.Cell(1, 3).Value = "Marca";
        ws.Cell(1, 4).Value = "Modelo";
        ws.Cell(1, 5).Value = "Serie";
        ws.Cell(1, 6).Value = "Ubicación";
        ws.Cell(1, 7).Value = "Custodio";
        ws.Cell(1, 8).Value = "Observaciones";

        int row = 2;

        foreach (var m in materiales)
        {
            ws.Cell(row, 1).Value = m.Numero;
            ws.Cell(row, 2).Value = m.Nombre;
            ws.Cell(row, 3).Value = m.Marca;
            ws.Cell(row, 4).Value = m.Modelo;
            ws.Cell(row, 5).Value = m.Serie;
            ws.Cell(row, 6).Value = m.Ubicacion;
            ws.Cell(row, 7).Value = m.Custodio;
            ws.Cell(row, 8).Value = string.IsNullOrWhiteSpace(m.Observaciones) ? "-" : m.Observaciones;
            row++;
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        wb.SaveAs(stream);
        return stream.ToArray();
    }


    public byte[] GenerarExcelEspacios(List<Espacio> espacios)
    {
        using var wb = new XLWorkbook();
        var ws = wb.AddWorksheet("Espacios");

        ws.Cell(1, 1).Value = "Nombre Espacio";
        ws.Cell(1, 2).Value = "Ubicación";
        ws.Cell(1, 3).Value = "Responsable";
        ws.Cell(1, 4).Value = "Observaciones";

        int row = 2;

        foreach (var e in espacios)
        {
            ws.Cell(row, 1).Value = e.NombreEspacio;
            ws.Cell(row, 2).Value = e.Ubicacion;
            ws.Cell(row, 3).Value = e.Responsable;
            ws.Cell(row, 4).Value = e.Observaciones;
            ws.Cell(row, 5).Value = string.IsNullOrWhiteSpace(e.Observaciones) ? "-" : e.Observaciones;
            row++;
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        wb.SaveAs(stream);
        return stream.ToArray();
    }



    public byte[] GenerarExcelAccesos(List<RegistroAcceso> accesos)
    {
        using var wb = new XLWorkbook();
        var ws = wb.AddWorksheet("Accesos");

        ws.Cell(1, 1).Value = "Fecha";
        ws.Cell(1, 2).Value = "Nombre";
        ws.Cell(1, 3).Value = "Cuenta";
        ws.Cell(1, 4).Value = "Hora Ingreso";
        ws.Cell(1, 5).Value = "Hora Salida";
        ws.Cell(1, 6).Value = "Estado";

        int row = 2;

        foreach (var r in accesos)
        {
            ws.Cell(row, 1).Value = r.Fecha.ToString("dd/MM/yyyy");
            ws.Cell(row, 2).Value = r.NombreCompleto;
            ws.Cell(row, 3).Value = r.NumeroCuenta;
            ws.Cell(row, 4).Value = r.HoraIngreso?.ToString(@"hh\:mm\:ss") ?? "-";
            ws.Cell(row, 5).Value = r.HoraSalida?.ToString(@"hh\:mm\:ss") ?? "-";
            ws.Cell(row, 6).Value = r.HoraSalida == null ? "Dentro" : "Salió";

            row++;
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        wb.SaveAs(stream);
        return stream.ToArray();
    }



    public byte[] GenerarExcelReservaEspacios(List<ReservaEspacio> reservaEspacios)
    {
        using var wb = new XLWorkbook();
        var ws = wb.AddWorksheet("Reservas");

        ws.Cell(1, 1).Value = "Espacio";
        ws.Cell(1, 2).Value = "Alumno";
        ws.Cell(1, 3).Value = "Profesor";
        ws.Cell(1, 4).Value = "Hora Inicio";
        ws.Cell(1, 5).Value = "Hora Fin";

        int row = 2;

        foreach (var r in reservaEspacios)
        {
            ws.Cell(row, 1).Value = r.Espacio.NombreEspacio.ToString();
            ws.Cell(row, 2).Value = r.Alumno?.Nombre.ToString();
            ws.Cell(row, 3).Value = r.Profesor?.NombreProfesor.ToString();
            ws.Cell(row, 4).Value = r.Fecha.ToString(@"hh\:mm\:ss") ?? "-";
            ws.Cell(row, 5).Value = r.HoraInicio.ToString();
            ws.Cell(row, 5).Value = r.HoraFin.ToString();


            row++;
        }

        ws.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        wb.SaveAs(stream);
        return stream.ToArray();
    }

}

