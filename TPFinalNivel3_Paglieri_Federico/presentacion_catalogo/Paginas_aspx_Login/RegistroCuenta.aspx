<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraWeb.Master" AutoEventWireup="true" CodeBehind="RegistroCuenta.aspx.cs" Inherits="presentacion_catalogo.Paginas_aspx_Login.RegistroCuenta" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="../Contenido_CSS/Login.css" />
     <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

  <%--  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>


    <section class="login">
        <section id="registro_usuario">
            <article class="usuario_contenedor_textBox">
                <asp:TextBox ID="tBoxUsuario"  TextMode="MultiLine" placeholder="Email" CssClass="login_info info_email" runat="server" MaxLength="100" required="required"></asp:TextBox>
            </article>
            
            <article class="usuario_contenedor_span">
                <span id="usuario_contenedor_label_error">
                    <%if (!UsuarioRepetido)%>
                    <%{%>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="tBoxUsuario" ErrorMessage="El correo debe terminar en @gmail.com"
                        ValidationExpression="^[a-zA-Z0-9._%+-]+@gmail\.com$" ForeColor="Red"> 
                    </asp:RegularExpressionValidator>
                    <%}%>
                   
                </span>
            </article>

        </section>

        <section id="registro_contraseña">
            <article class="contraseña_contenedor_textBox-Registro">
                <asp:TextBox ID="tBoxContraseña" TextMode="Password" placeholder="Contraseña" CssClass="login_info" runat="server" MaxLength="20" required="required"></asp:TextBox>
            </article>



        </section>

        <div class="contenedor_bton_registrarse">
            <asp:Button ID="btonAgregarUsuario" CssClass="login_acceder" OnClick="btonAgregarUsuario_Click" runat="server" Text="Crear Cuenta" />
        </div>





        <% if (UsuarioRepetido) %>

        <% { %>

        <script>
            
            function CrearLabelAlerta() {

                const usuarioContenedorLabelError = document.getElementById("usuario_contenedor_label_error");
                const tBoxUsuario = document.getElementById("<%= tBoxUsuario.ClientID%>");


                const labelUsuario = document.createElement("label");
                labelUsuario.setAttribute("for", tBoxUsuario.id)
                labelUsuario.textContent = "Usuario existente";
                labelUsuario.className = "label--usuario-creado";


                const iconErrorUsuario = document.createElement("i");
                iconErrorUsuario.className = "fa-solid fa-circle-exclamation";





                usuarioContenedorLabelError.appendChild(iconErrorUsuario);


                usuarioContenedorLabelError.appendChild(labelUsuario);





                DuracionLabelAlerta(usuarioContenedorLabelError);
            }



            function DuracionLabelAlerta(usuarioContenedorLabelError) {
                setTimeout(() => {
                    usuarioContenedorLabelError.style.opacity = "0";

                }, 3000)

            }

            CrearLabelAlerta();
           

        </script>

        <% } %>
    </section>





</asp:Content>
