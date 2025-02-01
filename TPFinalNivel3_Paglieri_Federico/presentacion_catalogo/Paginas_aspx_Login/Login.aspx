<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraWeb.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="presentacion_catalogo.Paginas_aspx_Login.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Contenido_CSS/Login.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    


    <section class="login">

        <section id="registro_usuario">

            <article class="usuario_contenedor_textBox">
                <asp:TextBox ID="tBoxUsuario" TextMode="MultiLine" placeholder="Email" CssClass="login_info info_email" runat="server" MaxLength="100" required="required"></asp:TextBox>
            </article>

            <article class="usuario_contenedor_span">
                <span id="usuario_contenedor_label_error"></span>
            </article>

        </section>


        <section id="registro_contraseña-login">

            <article class="contraseña_contenedor_textBox">
                <asp:TextBox ID="tBoxContraseña" TextMode="Password" placeHolder="Contraseña" CssClass="login_info" runat="server" MaxLength="20" required="required"></asp:TextBox>
            </article>

            <article class="contraseña_contenedor_span">
                <span id="contraseña_contenedor_label_error"></span>
            </article>


        </section>



        <div class="contenedor_bton_registrarse">
            <asp:Button ID="btonLogin" CssClass="login_acceder" runat="server" OnClick="btonLogin_Click" Text="Login" />
        </div>

        <div class="contenedor_contenedor_label_login_cuentaInexistente_crear_cuenta">


            <%if (Session["validarVisibilidadLabel"] != null)%>
            <%{%>
            <%if ((bool)Session["validarVisibilidadLabel"] == true) %>
            <% { %>
            <div class="contenedor_label_login_cuentaInexistente_crear_cuenta">
                <asp:LinkButton ID="linkRegistro" runat="server" Text="Si no tiene una cuenta puede crearla aquí" OnClick="linkRegistro_Click"></asp:LinkButton>
            </div>

            <% } %>
            <%} %>
        </div>




        <%if (!loginvalidado)
            {%>

        <script>
            
            function CrearLabelAlerta() {

                const usuarioContenedorLabelError = document.getElementById("usuario_contenedor_label_error");
                const contraseñaContenedorLabelError = document.getElementById("contraseña_contenedor_label_error");
                const tBoxUsuario = document.getElementById("<%= tBoxUsuario.ClientID %>");
                const tBoxContraseña = document.getElementById("<%= tBoxContraseña.ClientID %>");

                const labelUsuario = document.createElement("label");
                labelUsuario.setAttribute("for", tBoxUsuario.id);
                labelUsuario.textContent = "Usuario o contraseña inexistente";
                labelUsuario.className = "label--usuario-creado";
                  

                const labelContraseña = document.createElement("label");
                labelContraseña.setAttribute("for", tBoxContraseña.id);
                labelContraseña.textContent = "Usuario o contraseña inexistente";
                labelContraseña.className = "label--contraseña-creada";
                 

                const iconErrorUsuario = document.createElement("i");
                iconErrorUsuario.className = "fa-solid fa-circle-exclamation";


                const iconErrorContraseña = document.createElement("i");
                iconErrorContraseña.className = "fa-solid fa-circle-exclamation";


                usuarioContenedorLabelError.appendChild(iconErrorUsuario);
                usuarioContenedorLabelError.appendChild(labelUsuario);

                contraseñaContenedorLabelError.appendChild(iconErrorContraseña);
                contraseñaContenedorLabelError.appendChild(labelContraseña);




                DuracionLabelAlerta(usuarioContenedorLabelError, contraseñaContenedorLabelError);
            }



            function DuracionLabelAlerta(usuarioContenedorLabelError, contraseñaContenedorLabelError) {
                setTimeout(() => {
                    usuarioContenedorLabelError.style.opacity = "0";
                    contraseñaContenedorLabelError.style.opacity = "0";

                }, 1200)

            }

            CrearLabelAlerta();


        </script>

        <a href="RegistroCuenta.aspx" class="login_registro">Registrarse</a>
        <%}%>
    </section>

</asp:Content>
