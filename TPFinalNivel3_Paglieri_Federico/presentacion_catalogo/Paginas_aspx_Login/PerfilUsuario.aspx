<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraWeb.Master" AutoEventWireup="true" CodeBehind="PerfilUsuario.aspx.cs" Inherits="presentacion_catalogo.Paginas_aspx_Login.PerfilUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link rel="stylesheet" href="../Contenido_CSS/PerfilUsuario.css" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--  <asp:ScriptManager ID="ScriptRegularExpression" runat="server"></asp:ScriptManager>--%>


    <section class="perfil">

        <%--  <asp:HiddenField ID="HiddenFieldNavBarResponsiveJSValidationPerfilUsuario" runat="server" />--%>

        <section class="perfil_datos">



            <article class="datos_contenedor_datos">
                <asp:Label   CssClass="datos_label_input" runat="server">Nombre</asp:Label>
                <asp:TextBox ID="tBoxNombre" CssClass="datos" placeholder="Nombre" runat="server" MaxLength="20"></asp:TextBox>
            </article>

            <asp:RegularExpressionValidator ID="validarFormatoTextBoxNombre" ControlToValidate="tBoxNombre"
                ValidationExpression="^[a-zA-Z0-9]+$" ErrorMessage="solo números y letras" runat="server"></asp:RegularExpressionValidator>



            <article class="datos_contenedor_datos">
                <asp:Label  CssClass="datos_label_input" runat="server">Apellido</asp:Label>
                <asp:TextBox ID="tBoxApellido" CssClass="datos" placeholder="Apellido" runat="server" MaxLength="20"></asp:TextBox>
            </article>

            <asp:RegularExpressionValidator ID="validarFormatoTextBoxApellido" ControlToValidate="tBoxApellido"
                ValidationExpression="^[a-zA-Z0-9]+$" ErrorMessage="solo números y letras" runat="server">
            </asp:RegularExpressionValidator>


            <%/*
El tBoxEmail y tBoxContraseña los dejo  temporalmente de solo lectura(ReadOnly) hasta qeu aprennda a usar mailtrap y 
configure algo para que llegue un mail de confirmacion al usuario para modificar esos datos. Ya que son datos sensibles.
Si permito cambiarlo asi nomas desde la pagina, es muy inseguro.
*/%>


            <article class="datos_contenedor_datos">
                <asp:Label  CssClass="datos_label_input" runat="server">Email</asp:Label>
                <asp:TextBox ID="tBoxEmail" TextMode="Email" CssClass="datos" ReadOnly="true" runat="server" MaxLength="20"></asp:TextBox>
            </article>




            <%-- <asp:UpdatePanel ID="panelCheckBoxCambiarContraseña" UpdateMode="Conditional" runat="server">
                <ContentTemplate>--%>

            <article class="datos_contenedor_datos-checkBox">
                <div style="display: flex; gap: 0.5rem; margin-right: 8rem">
                    <asp:Label  CssClass="datos_label_input label_check" runat="server">Cambiar Contraseña</asp:Label>
                    <asp:CheckBox ID="cBoxCambiarContraseña" AutoPostBack="true" OnCheckedChanged="cBoxCambiarContraseña_CheckedChanged" runat="server" />
                </div>

            </article>





            <section class="contenedor_tBoxContraseñas_GuardarContraseña_GuardarDatos_Eliminar">


                <%if (Session["CheckBoxChecked"] != null)%>
                <%{%>
                <%if ((bool)Session["CheckBoxChecked"]) %>
                <%{%>






                <section id="contenedor_contraseñas" class="section_contraseñas">

                    <article id="contenedor_contraseña_actual" class="datos_contenedor_datos">

                        <asp:Label  ID="label_Contraseña_Actual" CssClass="datos_label_input" runat="server">Contraseña Actual</asp:Label>
                        <asp:TextBox ID="tBoxContraseñaActual" TextMode="Password" required="true" placeholder="Ingrese su contraseña actual" CssClass="datos datos-contraseña" runat="server" MaxLength="20"></asp:TextBox>




                    </article>

                    <article class="datos_contenedor_datos">
                        <asp:Label  CssClass="datos_label_input" runat="server">Contraseña Nueva</asp:Label>
                        <asp:TextBox ID="tBoxContraseñaNueva" TextMode="Password" required="true" placeholder="Ingrese la nueva contraseña" CssClass="datos datos-contraseña" runat="server" MaxLength="20"></asp:TextBox>
                    </article>




                </section>





                <%}%>
                <%}%>
                <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>


                <%if (Session["CheckBoxChecked"] != null)%>
                <%{%>
                <%if ((bool)Session["CheckBoxChecked"]) %>
                <%{%>
                <asp:Panel ID="panel_guardar_contraseña" CssClass="Contenedor_Contenedor_Bton_Guardar_Contraseña" runat="server">

                    <asp:Button ID="btonGuardarContraseaña" CssClass="datos_guardar guardar_contraseña" Text="Guardar contraseña" runat="server" OnClick="btonGuardarContraseaña_Click"></asp:Button>




                </asp:Panel>
                <%}%>
                <%} %>


                <%if (Session["ValidarGuardarCambios"] != null)%>
                <%{%>
                <%if (!(bool)Session["ValidarGuardarCambios"]) %>
                <% { %>
                <script>
                    const labelContraseñaActual = document.getElementById("ContentPlaceHolder1_label_Contraseña_Actual");
                    const tBoxContraseñaActual = document.getElementById("<%= tBoxContraseñaActual.ClientID%>");

                    labelContraseñaActual.setAttribute("for", tBoxContraseñaActual.id); 
                    labelContraseñaActual.textContent = "Contraseña Incorrecta";
                    labelContraseñaActual.style.color = "orangered";
                    

                    setTimeout(() => {
                        labelContraseñaActual.style.opacity = 0;
                    }, 2000)

                    setTimeout(() => {
                        labelContraseñaActual.style.opacity = 1;
                        labelContraseñaActual.textContent = "Contraseña Actual";
                        labelContraseñaActual.style.color = "dodgerblue";
                    }, 2500)

                </script>

                <% } %>

                <%} %>







                <section class="contenedor_buttons">
                    <!--Si los siguientes buttopn los dejo dentro de un updatePanel, la etiqueta asp FileUpload no funcionará bien ya
                   que requiere una actualización de toda la página. Si pongo los siguientes botones dentro de un updatePanel
                   entonces se hará una actualización parcial, y aunque haya seleccionado un archivo en el FileUpload, la propiedad
                   HasFile aparecerá en false-->

                    <asp:Button ID="btonGuardarCambios" CssClass="datos_guardar" runat="server" Text="Guardar cambios" OnClick="btonGuardarCambios_Click" />


                    <asp:Button ID="btonEliminarCuenta" CssClass="datos_guardar datos_eliminar" runat="server" Text="Eliminar cuenta" OnClick="btonEliminarCuenta_Click" />


                </section>
            </section>






        </section>


        <figure class="perfil_imagen">

            <asp:FileUpload ID="InputFile" CssClass="imagen_seleccionar" accept="image/jpeg,image/png" runat="server" />

            <asp:Image ID="imgPerfil" CssClass="imagen_imagen" ImageUrl="~/Imagenes/DefaultImage.png" runat="server" />

            <asp:HiddenField ID="HiddenFieldEliminarImagen" runat="server" />
            <script>

                const Img = document.getElementById("<%= imgPerfil.ClientID%>")
                const placeholderUrl = '<%= ResolveUrl("~/Imagenes/DefaultImage.png") %>';
                const hiddenFieldEliminarImagen = document.getElementById("<%= HiddenFieldEliminarImagen.ClientID%>");

                let iconEliminarImg = document.createElement("i");
                iconEliminarImg.classList.add("fas", "fa-trash", "icon_eliminar_img_perfil");

                let label = document.createElement("label");
                label.classList.add("datos_label_input");
                label.textContent = "Eliminar Imágen";
                label.style.fontSize = "1.7rem";
                label.style.visibility = "hidden";

                iconEliminarImg.addEventListener("mouseenter", event => {
                    label.style.visibility = "visible";
                })

                iconEliminarImg.addEventListener("mouseleave", event => {
                    label.style.visibility = "hidden";
                })

                iconEliminarImg.addEventListener("click", event => {
                    hiddenFieldEliminarImagen.value = "true";
                    __doPostBack("<%= HiddenFieldEliminarImagen.ClientID%>", "")

                })


                let section = document.createElement("section");

                section.appendChild(label);
                section.appendChild(iconEliminarImg);
                section.style.cssText = "display: flex; flex-direction: row; gap: 1rem; width: 100%; align-items: center; justify-content: flex-end";


                Img.insertAdjacentElement("beforebegin", section);


            </script>

        </figure>





    </section>





    <script src="../Contenido_JS/PerfilUsuario.js"></script>


</asp:Content>
