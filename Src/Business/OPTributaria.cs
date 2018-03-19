/*
    This file is part of the EasySII (R) project.
    Copyright (c) 2017-2018 Irene Solutions SL
    Authors: Irene Solutions SL.

    This program is free software; you can redistribute it and/or modify
    it under the terms of the GNU Affero General Public License version 3
    as published by the Free Software Foundation with the addition of the
    following permission added to Section 15 as permitted in Section 7(a):
    FOR ANY PART OF THE COVERED WORK IN WHICH THE COPYRIGHT IS OWNED BY
    IRENE SOLUTIONS SL. IRENE SOLUTIONS SL DISCLAIMS THE WARRANTY OF NON INFRINGEMENT
    OF THIRD PARTY RIGHTS
    
    This program is distributed in the hope that it will be useful, but
    WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
    or FITNESS FOR A PARTICULAR PURPOSE.
    See the GNU Affero General Public License for more details.
    You should have received a copy of the GNU Affero General Public License
    along with this program; if not, see http://www.gnu.org/licenses or write to
    the Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor,
    Boston, MA, 02110-1301 USA, or download the license from the following URL:
        http://www.irenesolutions.com/terms-of-use.pdf
    
    The interactive user interfaces in modified source and object code versions
    of this program must display Appropriate Legal Notices, as required under
    Section 5 of the GNU Affero General Public License.
    
    You can be released from the requirements of the license by purchasing
    a commercial license. Buying such a license is mandatory as soon as you
    develop commercial activities involving the EasySII software without
    disclosing the source code of your own applications.
    These activities include: offering paid services to customers as an ASP,
    serving extract PDFs data on the fly in a web application, shipping EasySII
    with a closed source product.
    
    For more information, please contact Irene Solutions SL. at this
    address: info@irenesolutions.com
 */

using EasySII.Tax;
using EasySII.Xml.Sii;
using EasySII.Xml.Silr;
using System;

namespace EasySII.Business
{
    /// <summary>
    /// Representa las Operaciones de Seguros.
    /// </summary>
    public class OPTributaria
    {

        /// <summary>
        /// Parte compradora que figura en la factura.
        /// </summary>
        public virtual Party SellerParty { get; set; }

        /// <summary>
        /// Cuando el identificador es distinto del NIF establece el tipo de identificador utilizado.
        /// </summary>
        public IDOtroType IDOtroType { get; set; }

        /// <summary>
        /// Código ISO del pais cuando el NIF no es español.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Clave de la operación (para las operaciones de seguros).
        /// </summary>
        public string ClaveOperacion { get; set; }

        /// <summary>
        /// Importe total (tanto para las operaciones de seguros como para los cobros en metálico
        /// </summary>
        public decimal GrossAmount { get; set; }

        /// <summary>
        /// Fecha de emisión de la operación. La necesitamos para posteriormente poder generar el Periodo Impositivo
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// Constructor OPTributarias.
        /// </summary>
        public OPTributaria()
        {
            IDOtroType = IDOtroType.OtroDocProbatorio;
        }


        /// <summary>
        /// Constructor de Insurance.
        /// </summary>
        /// <param name="registroLROperacionesSeguros">Objeto serialización xml facturas emitidas.</param>
        public OPTributaria(RegistroLROpTrascendTribu registroLROperacionesSeguros)
        {

            RegistroLROpTrascendTribu siiTributo= registroLROperacionesSeguros;

            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
                IssueDate = Convert.ToDateTime(siiTributo.PeriodoImpositivo.Ejercicio); // Necesitamos una fecha para calcular el Ejercicio/Periodo
            else
                IssueDate = Convert.ToDateTime(siiTributo.PeriodoLiquidacion.Ejercicio); // Necesitamos una fecha para calcular el Ejercicio/Periodo

            

            SellerParty = new Party()
            {
                PartyName = siiTributo.Contraparte.NombreRazon
            };

            if (siiTributo.Contraparte.IDOtro != null)
            {

                IDOtroType = (IDOtroType)Convert.ToInt32(siiTributo.Contraparte.IDOtro.IDType);

                CountryCode = siiTributo.Contraparte.IDOtro.CodigoPais;
                SellerParty.TaxIdentificationNumber = siiTributo.Contraparte.IDOtro.ID;

            }


            if (SellerParty == null)
                throw new ArgumentNullException("SellerParty is null.");

            ClaveOperacion = siiTributo.ClaveOperacion;
            GrossAmount = Convert.ToDecimal(siiTributo.ImporteTotal, Settings.DefaultNumberFormatInfo);

        }

        /// <summary>
        /// Obtiene un objeto RegistroLROpTranscendTribu, este objeto se utiliza
        /// para la serialización xml.
        /// </summary>
        /// <returns>Nueva instancia del objeto para serialización 
        /// xml RegistroLROpTranscendTribu.</returns>
        internal RegistroLROpTrascendTribu ToSII()
        {

            RegistroLROpTrascendTribu siiTributo= new RegistroLROpTrascendTribu();

            if (IssueDate == null)
                throw new ArgumentNullException("IssueDate is null.");

            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
            {
                siiTributo.PeriodoImpositivo.Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
                siiTributo.PeriodoImpositivo.Periodo = (IssueDate ?? new DateTime(1, 1, 1)).ToString("MM");
            }
            else
            {
                siiTributo.PeriodoLiquidacion.Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
                siiTributo.PeriodoLiquidacion.Periodo = (IssueDate ?? new DateTime(1, 1, 1)).ToString("MM");
            }

            if (SellerParty == null)
                throw new ArgumentNullException("SellerParty is null.");

            TaxIdEs taxIdEs = null;
            bool IsNotNifES = false;

            try
            {
                taxIdEs = new TaxIdEs(SellerParty.TaxIdentificationNumber);
            }
            catch
            {
                IsNotNifES = true;
            }

            if (taxIdEs != null)
                IsNotNifES = !taxIdEs.IsDCOK;

            if (SellerParty != null)
            {

                siiTributo.Contraparte.NombreRazon = SellerParty.PartyName;

                if (IsNotNifES)
                {

                    if (CountryCode == null)
                        throw new ArgumentNullException(
                            "For foreign tax identificator number Country Code can't be null");

                    siiTributo.Contraparte.IDOtro = new IDOtro();
                    siiTributo.Contraparte.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');
                    siiTributo.Contraparte.IDOtro.CodigoPais = CountryCode;
                    siiTributo.Contraparte.IDOtro.ID = SellerParty.TaxIdentificationNumber;

                }
                else
                {
                    siiTributo.Contraparte.NIF = SellerParty.TaxIdentificationNumber;
                }
            }


            siiTributo.ClaveOperacion = ClaveOperacion;
            siiTributo.ImporteTotal = GrossAmount.ToString(Settings.DefaultNumberFormatInfo);

            return siiTributo;

        }


        internal FiltroConsultaOTT ToFilterSII()
        {

            FiltroConsultaOTT siiFilter = new FiltroConsultaOTT();

            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
            {
                siiFilter.PeriodoImpositivo.Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
                siiFilter.PeriodoImpositivo.Periodo = (IssueDate ?? new DateTime(1, 1, 1)).ToString("MM");
            }
            else
            {
                siiFilter.PeriodoLiquidacion.Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
                siiFilter.PeriodoLiquidacion.Periodo = (IssueDate ?? new DateTime(1, 1, 1)).ToString("MM");
            }

            TaxIdEs taxIdEs = null;
            bool IsNotNifES = false;

            try
            {
                taxIdEs = new TaxIdEs(SellerParty.TaxIdentificationNumber);
            }
            catch
            {
                IsNotNifES = true;
            }

            if (taxIdEs != null)
                IsNotNifES = !taxIdEs.IsDCOK;

            if (SellerParty != null)
            {

                siiFilter.ClavePaginacion = new ClavePaginacionOTT();
                siiFilter.ClavePaginacion.Contraparte.NombreRazon = SellerParty.PartyName;

                if (IsNotNifES)
                {

                    if (CountryCode == null)
                        throw new ArgumentNullException(
                            "For foreign tax identificator number Country Code can't be null");

                    siiFilter.ClavePaginacion.Contraparte.IDOtro = new IDOtro();
                    siiFilter.ClavePaginacion.Contraparte.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');
                    siiFilter.ClavePaginacion.Contraparte.IDOtro.CodigoPais = CountryCode;
                    siiFilter.ClavePaginacion.Contraparte.IDOtro.ID = SellerParty.TaxIdentificationNumber;

                }
                else
                {
                    siiFilter.ClavePaginacion.Contraparte.NIF = SellerParty.TaxIdentificationNumber;
                }

                if (ClaveOperacion != null)
                    siiFilter.ClavePaginacion.ClaveOperacion = ClaveOperacion;

            }

            return siiFilter;

        }

        internal RegistroLROpTrascendTribu ToRegistroLRBajaOpTrascendTribuSII()
        {

            RegistroLROpTrascendTribu siiDelete = new RegistroLROpTrascendTribu();

            if (SellerParty == null)
                throw new ArgumentNullException("SellerParty is null.");

            if (Settings.Current.IDVersionSii.CompareTo("1.1") < 0)
            {
                siiDelete.PeriodoImpositivo.Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
                siiDelete.PeriodoImpositivo.Periodo = (IssueDate ?? new DateTime(1, 1, 1)).ToString("MM");
            }
            else
            {
                siiDelete.PeriodoLiquidacion.Ejercicio = (IssueDate ?? new DateTime(1, 1, 1)).ToString("yyyy");
                siiDelete.PeriodoLiquidacion.Periodo = (IssueDate ?? new DateTime(1, 1, 1)).ToString("MM");
            }

            TaxIdEs taxIdEs = null;
            bool IsNotNifES = false;

            try
            {
                taxIdEs = new TaxIdEs(SellerParty.TaxIdentificationNumber);
            }
            catch
            {
                IsNotNifES = true;
            }

            if (taxIdEs != null)
                IsNotNifES = !taxIdEs.IsDCOK;


            if (SellerParty != null)
            {

                siiDelete.Contraparte = new Contraparte();
                siiDelete.Contraparte.NombreRazon = SellerParty.PartyName;

                if (IsNotNifES)
                {

                    if (CountryCode == null)
                        throw new ArgumentNullException(
                            "For foreign tax identificator number Country Code can't be null");

                    siiDelete.Contraparte.IDOtro = new IDOtro();
                    siiDelete.Contraparte.IDOtro.IDType = ((int)IDOtroType).ToString().PadLeft(2, '0');
                    siiDelete.Contraparte.IDOtro.CodigoPais = CountryCode;
                    siiDelete.Contraparte.IDOtro.ID = SellerParty.TaxIdentificationNumber;

                }
                else
                {
                    siiDelete.Contraparte.NIF = SellerParty.TaxIdentificationNumber;
                }

                if (ClaveOperacion != null)
                    siiDelete.ClaveOperacion = ClaveOperacion;

            }

            return siiDelete;

        }

    }
}
