using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace tp_final_Nivel3_sostaric_patricio
{
    public  static class Validacion
    {
        public static bool ValidaTextoVacio(object control)
        {
            if (control is TextBox texto)
            {
                if (string.IsNullOrEmpty(texto.Text))
                    return true;
                else
                    return false;

            }


            return false;
        }

        // Valida que un TextBox contenga un número entero válido
        public static bool ValidaEntero(object control, out int valor)
        {
            valor = 0;
            if (control is TextBox texto)
            {
                return int.TryParse(texto.Text, out valor);
            }
            return false;
        }

        // Valida que un TextBox contenga un número decimal válido (ej: precio)
        public static bool ValidaDecimal(object control, out decimal valor)
        {
            valor = 0;
            if (control is TextBox texto)
            {
                return decimal.TryParse(texto.Text, out valor);
            }
            return false;
        }

        // Valida que un DropDownList tenga un valor seleccionado distinto de vacío
        public static bool ValidaDropDown(object control)
        {
            if (control is DropDownList ddl)
            {
                return ddl.SelectedIndex > 0; // índice 0 suele ser "Seleccione..."
            }
            return false;
        }

        // Valida que un CheckBox esté marcado (si es requerido)
        public static bool ValidaCheckBox(object control)
        {
            if (control is CheckBox chk)
            {
                return chk.Checked;
            }
            return false;
        }
    }




}