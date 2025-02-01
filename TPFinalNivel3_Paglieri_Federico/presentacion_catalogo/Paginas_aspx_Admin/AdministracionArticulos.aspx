<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraWeb.Master" AutoEventWireup="true" CodeBehind="AdministracionArticulos.aspx.cs" Inherits="presentacion_catalogo.Paginas_aspx_Admin.AdministracionArticulos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Contenido_CSS/AdministracionArticulos.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    



    <section class="contenedor_formulario">




        <div class="formulario_datos">


            <asp:UpdatePanel ID="updatePanelDetalleArticulos" runat="server">
                <ContentTemplate>






                    <asp:TextBox ID="tBoxId" CssClass="datos_input" ReadOnly="true" placeholder="id" AutoPostBack="false" runat="server"></asp:TextBox>
                    <article class="contenedor_dato">

                        <label id="lblMaxCaracteresNombre" for="tBoxNombre" class="maxCaracteres">Máxima cantidad de caracteres</label>
                        <asp:TextBox ID="tBoxNombre" CssClass="datos_input" placeholder="Nombre" ClientIDMode="Static"  MaxLength="50"  runat="server"></asp:TextBox>

                        
                        
                    </article>


                    <article class="contenedor_dato">

                        <label id="lblMaxCaracteresCodigoArticulo" for="tBoxCodigoArticulo" class="maxCaracteres">Máxima cantidad de caracteres</label>
                        <asp:TextBox ID="tBoxCodigoArticulo" CssClass="datos_input" ClientIDMode="Static" placeholder="Codigo del Artículo"  MaxLength="50"  runat="server"></asp:TextBox>
                        
                        
                       
                    </article>


                    <article class="contenedor_dato">

                        <label id="lblMaxCaracteresDescripcion" for="tBoxDescripcion" class="maxCaracteres">Máxima cantidad de caracteres</label>
                        <asp:TextBox ID="tBoxDescripcion" CssClass="datos_input input_MultiLine" ClientIDMode="Static" placeholder="Descripción"  MaxLength="150" TextMode="MultiLine"  runat="server"></asp:TextBox>

                        
                        
                        
                    </article>

                    <asp:DropDownList ID="dDownListMarca" CssClass="datos_option" AutoPostBack="true" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="dDownListCategoria" CssClass="datos_option" AutoPostBack="true" runat="server"></asp:DropDownList>

                    <article class="contenedor_dato">

                        <label id="lblMaxCaracteresPrecio" for="tBoxPrecio" class="maxCaracteres">Máxima cantidad de caracteres</label>
                        <asp:TextBox ID="tBoxPrecio" CssClass="datos_input" ClientIDMode="Static" placeholder="Precio"   MaxLength="20"  runat="server"></asp:TextBox>

                        <span class="contenedor_lbl_fuera_de_rango_precio">
                            <asp:Label  ID="lblErrorPrecio" CssClass="lbl_fuera_de_rango_precio" ClientIDMode="Static" ForeColor="Red"  runat="server"></asp:Label>
                        </span>


                       
                        
                        
                    </article>
                </ContentTemplate>
            </asp:UpdatePanel>


          

                    <asp:Panel ID="PanelButtonsAgregarEliminar" ClientIDMode="Static" runat="server">
                        <%if (Session["validarTipoUsuarioAdmin"] != null)%>
                        <%{%>
                        <%if ((bool)Session["validarTipoUsuarioAdmin"])%>
                        <%{

                        %>

                        <asp:Button ID="btonAgregar" CssClass="datos_add" OnClick="btonAgregar_Click" runat="server" Text="Agregar" />


                        <%if ((bool)Session["validarTipoUsuarioAdmin"] && Request.QueryString["id"] != null)%>
                        <%{%>
                        <asp:Button ID="btonEliminar" CssClass="datos_delete" OnClick="btonEliminar_Click" runat="server" Text="Eliminar" />
                        <%}%>
                        <%}%>
                        <%}%>
                    </asp:Panel>
           




        </div>



        <figure class="formulario_image">
            <%if (imgUrl != string.Empty || (imgUrl == string.Empty && Request.QueryString["Id"] != null))%>
            <%{%>
            <img src="<%= (string)Session["urlImagen"]%>" cssclass="image_articuloImg" onerror="handleImageError(this)" />

            <script>
                const imgArticulo = document.querySelector(".image_articuloImg");
                const contenedorImage = document.querySelector(".formulario_image");

                function handleImageError(imgElement) {
                    imgElement.src = "../Imagenes/DefaultImage.png";
                    contenedorImage.style.cssText = "background-color: transparent";
                }
            </script>
            <%}

            %>
        </figure>


        <%--Imagen alternativa que se coloca en el elemeto image en el caso de que por algún motivo no cargue la imágen principal.--%>
    </section>

    <%if (Session["validarTipoUsuarioAdmin"] != null)%>
    <%{%>
    <%if (!(bool)Session["validarTipoUsuarioAdmin"])%>

    <% {%>

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            const inputs = document.querySelectorAll('.datos_input');
            inputs.forEach(function (input) {
                input.readOnly = true;
            });

            const dropDowns = document.querySelectorAll(".datos_option");
            dropDowns.forEach(dropDown => {
                dropDown.disabled = true;
            })
        });
    </script>



    <%} %>
    <%}%>

    <script src="../Contenido_JS/AdministracionArticulos.js"></script>

</asp:Content>
