# EasySII
EasySII la biblioteca .net para el SII de la AEAT creada por [Irene Solutions](http://www.irenesolutions.com) . La finalidad de esta herramienta es simplificar los trabajos de serialización xml, envío y recepción de lotes para los libros registros.
Si necesita soporte o tiene alguna sugerencia, por favor, nos lo puede hacer saber en support@irenesolutions.com.



Bienvenido a la herramienta de EasySII! Gestiona con facilidad tu comunicación con el SII de la AEAT, por ejemplo, enviar un lote de facturas recibidas es tan fácil como esto:

# Lotes facturas recibidas

## Ejemplo del envío de un lote de facturas recibidas
### Se crean dos facturas emitidas (APInvoice) y se envían al SII de la AEAT

```C#
// A incluir:           
//using EasySII;
//using EasySII.Business;
//using EasySII.Business.Batches;
//using EasySII.Net;

// Creamos al titular del lote.
Party titular = new Party()
{
   TaxIdentificationNumber = "B12959755",
   PartyName = "IRENE SOLUTIONS SL"
};

// Lote de factura recibidas a enviar la AEAT al SII
var LoteFacturasRecibidas = new Batch(BatchActionKeys.LR, BatchActionPrefixes.SuministroLR, BatchTypes.FacturasRecibidas); 
LoteFacturasRecibidas.Titular = titular;
LoteFacturasRecibidas.CommunicationType = CommunicationType.A0; // Alta de facturas:
// utilizando el tipo de comunicación podemos modificar datos de facturas envíadas
// anteriormente. En lugar de alta de facturas, podemos elegir modificación.
            
Party comprador = titular; // El titular es el comprador en este caso

APInvoice facturaRecibidaPrimera = new APInvoice(); // Primera factura 

facturaRecibidaPrimera.IssueDate = new DateTime(2018, 1, 10); // Fecha de emisión factura
facturaRecibidaPrimera.PostingDate = new DateTime(2018, 1, 31); // Fecha de contabilización: Se utiliza para serializar el periodo (2018.01)
facturaRecibidaPrimera.RegisterDate = new DateTime(2018, 2, 5); // Fecha de registro: Se utiliza para serializar la fecha de registro

facturaRecibidaPrimera.SellerParty = new Party() // Acreedor (Emisor factura)
{
   TaxIdentificationNumber = "B12756474",
   PartyName = "MAC ORGANIZACION SL"
};

facturaRecibidaPrimera.BuyerParty = comprador; // Comprador
facturaRecibidaPrimera.InvoiceNumber = "FR/00003"; // Número de factura
facturaRecibidaPrimera.InvoiceType = InvoiceType.F1; // Tipo de factura
facturaRecibidaPrimera.ClaveRegimenEspecialOTrascendencia =
ClaveRegimenEspecialOTrascendencia.RegimenComun;
facturaRecibidaPrimera.GrossAmount = 121m; // Importe bruto
facturaRecibidaPrimera.InvoiceText = "Licencia software"; // Descripción     

facturaRecibidaPrimera.AddTaxOtuput(21, 100, 21);
LoteFacturasRecibidas.BatchItems.Add(facturaRecibidaPrimera); // Añadimos la segunda factura al lote

APInvoice facturaRecibidaSegunda = new APInvoice(); // Segunda factura (Exenta)

facturaRecibidaSegunda.IssueDate = new DateTime(2018, 1, 15); // Fecha de emisión factura
facturaRecibidaSegunda.PostingDate = new DateTime(2018, 1, 31); // Fecha de contabilización: Se utiliza para serializar el periodo (2017.01)
facturaRecibidaSegunda.RegisterDate = new DateTime(2018, 2, 6); // Fecha de registro: Se utiliza para serializar la fecha de registro

facturaRecibidaSegunda.SellerParty = new Party() // Acreedor (Emisor factura)
{
   TaxIdentificationNumber = "B12756474",
   PartyName = "MAC ORGANIZACION SL"
};
facturaRecibidaSegunda.BuyerParty = comprador; // Comprador
facturaRecibidaSegunda.InvoiceNumber = "FRA/00004"; // Número de factura
facturaRecibidaSegunda.InvoiceType = InvoiceType.F1; // Tipo de factura
facturaRecibidaSegunda.ClaveRegimenEspecialOTrascendencia =
ClaveRegimenEspecialOTrascendencia.RegimenComun;
facturaRecibidaSegunda.GrossAmount = 100m; // Importe bruto
facturaRecibidaSegunda.InvoiceText = "Intereses préstamo"; // Descripción  
            
// Si no se incluyen líneas de IVA considera GrossAmount como exenta
         
LoteFacturasRecibidas.BatchItems.Add(facturaRecibidaSegunda); // Añadimos la segunda factura al lote

// Realizamos el envío del lote a la AEAT
string response = BatchDispatcher.SendSiiLote(LoteFacturasRecibidas);

foreach (var factura in LoteFacturasRecibidas.BatchItems)
{
    string msg = "";
    if (factura.Status == "Correcto" || factura.Status == "AceptadoConErrores")
        msg = $"El estado del envío es: {factura.Status} y el código CSV: {factura.CSV}";
    else
        msg = $"El estado del envío es: {factura.Status}, error: {factura.ErrorCode} '{factura.ErrorMessage}'";

    // Continuar según resultado

}

```

![Irene Solutions SL](http://www.irenesolutions.com/archive/img/logo-irene-solutions-transparent-sm.png)

* [Introducción](https://github.com/mdiago/EasySII/wiki/000---Introducci%C3%B3n)

* Ejemplos


   * [Configuración del certificado para la comunicación con la AEAT](https://github.com/mdiago/EasySII/wiki/001---Ejemplo:-Configuraci%C3%B3n-del-certificado-para-la-comunicaci%C3%B3n-con-la-AEAT)

   * [Creación de lotes de facturas recibidas](https://github.com/mdiago/EasySII/wiki/002---Ejemplo:-Creaci%C3%B3n-de-lotes-de-facturas-recibidas)
   
    * [Creación de lotes de facturas emitidas](https://github.com/mdiago/EasySII/wiki/003---Ejemplo:-Creaci%C3%B3n-de-lotes-de-facturas-emitidas)


   * [Creación de lotes de bienes de inversión](https://github.com/mdiago/EasySII/wiki/004---Ejemplo:-Creaci%C3%B3n-de-lotes-de-bienes-de-inversi%C3%B3n)

   * [Creación de lotes de pagos facturas recibidas](https://github.com/mdiago/EasySII/wiki/005---Ejemplo:-Creaci%C3%B3n-de-lotes-de-pagos-facturas-recibidas) 

   * [Creación de lotes de cobros facturas emitidas](https://github.com/mdiago/EasySII/wiki/006---Ejemplo:-Creaci%C3%B3n-de-lotes-de-cobros-facturas-emitidas)

   * [Borrado de facturas recibidas](https://github.com/mdiago/EasySII/wiki/007---Ejemplo:-Borrado-de-facturas-recibidas)
 
   * [Borrado de facturas emitidas](https://github.com/mdiago/EasySII/wiki/008---Ejemplo:-Borrado-de-facturas-emitidas)

   * [Borrado de pagos de facturas recibidas](https://github.com/mdiago/EasySII/wiki/009---Ejemplo:-Borrado-de-pagos-de-facturas-recibidas)

   * [Borrado de cobros de facturas emitidas](https://github.com/mdiago/EasySII/wiki/010---Ejemplo:-Borrado-de-cobros-de-facturas-emitidas)

   * [Borrado de bienes de inversión](https://github.com/mdiago/EasySII/wiki/011---Ejemplo:-Borrado-de-bienes-de-inversi%C3%B3n)

   * [Creación de lotes de cobros en metálico](https://github.com/mdiago/EasySII/wiki/012---Ejemplo:-Creaci%C3%B3n-de-lotes-de-cobros-en-met%C3%A1lico)

   * [Borrado de cobros en metálico](https://github.com/mdiago/EasySII/wiki/013---Ejemplo:-Borrado-de-cobros-en-met%C3%A1lico)

   * [Consulta del libro registro de emitidas y recibidas](https://github.com/mdiago/EasySII/wiki/014-Ejemplo:-Consulta-del-libro-registro-de-emitidas-y-recibidas)

   * [Consulta del libro registro de facturas recibidas por clientes](https://github.com/mdiago/EasySII/wiki/015-Ejemplo:-Consulta-del-libro-registro-de-facturas-recibidas-por-clientes)

   * [Consulta del libro registro de facturas emitidas por proveedores](https://github.com/mdiago/EasySII/wiki/016-Ejemplo:-Consulta-del-libro-registro-de-facturas-emitidas-por-proveedores)

   * [Factura emitida con NIF extranjero no español](https://github.com/mdiago/EasySII/wiki/017-Ejemplo:-Factura-emitida-con-NIF-extranjero-no-espa%C3%B1ol)

   * [Factura a la Administración con IVA diferido](https://github.com/mdiago/EasySII/wiki/018-Ejemplo:-Factura-a-la-Administraci%C3%B3n-con-IVA-diferido)

   * [Factura emitida no sujeta por artículo 7, 14 u otros](https://github.com/mdiago/EasySII/wiki/019-Ejemplo:-Factura-emitida-no-sujeta-por-art%C3%ADculo-7,-14-u-otros)

   * [Factura emitida no sujeta reglas localización](https://github.com/mdiago/EasySII/wiki/019.b-Ejemplo:-Factura-emitida-no-sujeta-reglas-localizaci%C3%B3n)

   * [Factura recibida REAGYP (régimen especial agricultura, ganadería y pesca)](https://github.com/mdiago/EasySII/wiki/020-Ejemplo:-Factura-recibida-REAGYP-(r%C3%A9gimen-especial-agricultura,-ganader%C3%ADa-y-pesca))

   * [Factura emitida con Inversión del Sujeto Pasivo](https://github.com/mdiago/EasySII/wiki/021-Ejemplo:-Factura-emitida-con-Inversi%C3%B3n-del-Sujeto-Pasivo)

   * [Suministro información Operaciones Seguros](https://github.com/mdiago/EasySII/wiki/022-Ejemplo:-Suministro-informaci%C3%B3n-Operaciones-Seguros)

   * [Factura emitida con Recargo de Equivalencia](https://github.com/mdiago/EasySII/wiki/023-Ejemplo:-Factura-emitida-con-Recargo-de-Equivalencia)

   * [Factura emitida con varias Causas de Exención](https://github.com/mdiago/EasySII/wiki/024-Ejemplo:-Factura-emitida-con-varias-Causas-de-Exenci%C3%B3n)

   * [Validación de NIF](https://github.com/mdiago/EasySII/wiki/051---Ejemplo:-Validaci%C3%B3n-de-NIF)
