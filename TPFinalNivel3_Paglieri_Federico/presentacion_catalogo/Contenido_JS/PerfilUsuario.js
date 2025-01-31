

// Selecciono el elemento header de la pagina maestra, para poder cambiar su color dependiendo de la página actual.

document.addEventListener("DOMContentLoaded", event => {





    const header = document.querySelector(".header");


    let paginaActual = window.location.pathname;





    if (paginaActual.includes("PerfilUsuario")) {

        let ValidarCaracteres = () => {

            let textBoxNombre = document.querySelector("#ContentPlaceHolder1_tBoxNombre");
            let textBoxApellido = document.querySelector("#ContentPlaceHolder1_tBoxApellido");
            let buttonGuardarCambios = document.querySelector("#ContentPlaceHolder1_btonGuardarCambios");
            /*
            los proximos dos objetos(porque los elementos html son objetos, serán los dos textBox de cambiar contraseña.)
             */

            // Función para validar el texto usando expresión regular
            let validarTexto = (texto) => {
                let regexp = /^[a-zA-Z0-9\s]*$/;
                return regexp.test(texto);
            };

            // Función para validar ambos campos y habilitar o deshabilitar el botón
            let validarCampos = () => {
                let nombreValido = validarTexto(textBoxNombre.value);
                let apellidoValido = validarTexto(textBoxApellido.value);

                // Habilitar el botón solo si ambos campos son válidos
                if (nombreValido && apellidoValido) {
                    if (buttonGuardarCambios) {
                        buttonGuardarCambios.style.visibility = "visible";
                    }
                   
                } else {
                    if (buttonGuardarCambios) {
                        buttonGuardarCambios.style.visibility = "hidden";
                    }
                   
                }
            };

            // Añadir eventos para validar los campos cuando cambien
            textBoxNombre.addEventListener('input', validarCampos);
            textBoxApellido.addEventListener('input', validarCampos);

            // Llamar a la función para establecer el estado inicial del botón
            validarCampos();

        }

        ValidarCaracteres();

    }








})