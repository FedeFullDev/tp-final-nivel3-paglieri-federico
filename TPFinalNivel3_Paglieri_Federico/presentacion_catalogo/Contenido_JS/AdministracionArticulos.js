document.addEventListener("DOMContentLoaded", () => {


    let validarPrecio = () => {

        var textBoxPrecio = document.getElementById('tBoxPrecio');
        var minMoney = -922337203685477.5808;
        var maxMoney = 922337203685477.5807;
        var panel = document.getElementById('PanelButtonsAgregarEliminar');
        var lblErrorPrecio = document.getElementById('lblErrorPrecio');
        var lblMaxCaracteresPrecio = document.getElementById('lblMaxCaracteresPrecio');

        // Verifica si los elementos existen antes de agregar eventos
        if (!textBoxPrecio || !panel || !lblErrorPrecio || !lblMaxCaracteresPrecio) {
            console.error("No se encontraron algunos elementos en el DOM.");
            return;
        }

        // Evento input para validar cuando el usuario escribe
        textBoxPrecio.addEventListener('input', () => {
            var value = parseFloat(textBoxPrecio.value);

            // Verifica si el valor es un número válido
            if (value < 0 || value > maxMoney) {
                lblErrorPrecio.innerText = "El número es menor a 0 o mayor que el límite";
                lblErrorPrecio.style.display = "block";
                panel.style.visibility = "hidden"; // Oculta el panel
            } else {
                lblErrorPrecio.style.display = "none";
                panel.style.visibility = "visible"; // Muestra el panel
            }

            //Validación para cantidad máxima de caracteres
            if (textBoxPrecio.value.length == 20) {

                lblMaxCaracteresPrecio.style.visibility = "visible";
            } else {
                lblMaxCaracteresPrecio.style.visibility = "hidden";

            }

            //Validación para formato incorrecto
            if (isNaN(value)) {
                lblErrorPrecio.innerHTML = "Formato incorrecto, sólo números";
                lblErrorPrecio.style.display = "block";
                panel.style.visibility = "hidden";
            }

            if (textBoxPrecio.value.trim() === "") {
                lblErrorPrecio.style.display = "none";
                panel.style.visibility = "visible";
                return; 
            }

        });

    }

    validarPrecio();

    let validarMaxCaracteresTextBox = () => {
        var textBoxNombre = document.getElementById('tBoxNombre');
        var lblMaxCaracteresNombre = document.getElementById('lblMaxCaracteresNombre');
        var textBoxCodigoArticulo = document.getElementById('tBoxCodigoArticulo');
        var lblMaxCaracteresCodigoArticulo = document.getElementById('lblMaxCaracteresCodigoArticulo');
        var textBoxDescripcion = document.getElementById('tBoxDescripcion');
        var lblMaxCaracteresDescripcion = document.getElementById('lblMaxCaracteresDescripcion');
        
        

        if (!textBoxNombre || !lblMaxCaracteresNombre) {
            return;
        } else {
            textBoxNombre.addEventListener('input', event => {

                if (textBoxNombre.value.length == 50) {
                    lblMaxCaracteresNombre.style.visibility = "visible";
                } else {
                    lblMaxCaracteresNombre.style.visibility = "hidden";

                }
            })
        }

        if (!textBoxCodigoArticulo || !lblMaxCaracteresCodigoArticulo) {
            return;
        } else {
            textBoxCodigoArticulo.addEventListener('input', event => {

                if (textBoxCodigoArticulo.value.length == 50) {
                    lblMaxCaracteresCodigoArticulo.style.visibility = "visible";
                } else {
                    lblMaxCaracteresCodigoArticulo.style.visibility = "hidden";

                }
            })
        }

        if (!textBoxDescripcion || !lblMaxCaracteresDescripcion) {
            return;
        } else {
            textBoxDescripcion.addEventListener('input', event => {

                if (textBoxDescripcion.value.length == 150) {
                    lblMaxCaracteresDescripcion.style.visibility = "visible";
                } else {
                    lblMaxCaracteresDescripcion.style.visibility = "hidden";

                }
            })
        }



    }

    validarMaxCaracteresTextBox();




});
