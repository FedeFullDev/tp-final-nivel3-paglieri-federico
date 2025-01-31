<%@ Page Title="" Language="C#" MasterPageFile="~/EstructuraWeb.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="presentacion_catalogo.Paginas_aspx_NoAdmin.Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="../Contenido_CSS/Home.css" rel="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--  <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>


    <asp:HiddenField ID="HiddenFieldNavBarResponsiveJSValidationHome" runat="server" />


    <script>

        /*Las siguientes líneas de código son para manejar el responsive de la página, por los cambios del viewport para distintos dispotivos*/
        const validadorViewPortNavBarHome = document.getElementById("<%= HiddenFieldNavBarResponsiveJSValidationHome.ClientID%>");
        const mediaQueryHome = window.matchMedia("(min-width: 240px) and (max-width: 750px)");
        const mediaQuery2Home = window.matchMedia("(min-width: 751px)");

        function handleMediaChangeHome() {

            if (mediaQueryHome.matches && validadorViewPortNavBarHome.value !== "true") {

                validadorViewPortNavBarHome.value = "true";
                __doPostBack("<%= HiddenFieldNavBarResponsiveJSValidationHome.ClientID %>", "");


            } else if (mediaQuery2Home.matches && validadorViewPortNavBarHome.value !== "false") {

                validadorViewPortNavBarHome.value = "false";
                __doPostBack("<%= HiddenFieldNavBarResponsiveJSValidationHome.ClientID %>", "");

            }
        }

        mediaQueryHome.addListener(handleMediaChangeHome);
        mediaQuery2Home.addListener(handleMediaChangeHome);




        handleMediaChangeHome();
    </script>


    <section class="contenedor_filtro">



        <p style="font-weight: 800; display: inline-block;">Filtro Rápido</p>
        <asp:TextBox ID="tBoxFiltroRapido" CssClass="filtro_texto" OnTextChanged="tBoxFiltroRapido_TextChanged" AutoPostBack="true" runat="server"></asp:TextBox>







        <section class="filtro_filtro">

            <p style="font-weight: 800; display: inline-block;">Filtro Avanzado</p>
            <asp:CheckBox ID="cBoxFiltroAvanzado" AutoPostBack="true" OnCheckedChanged="CheckBoxFiltroAvanzado_CheckedChanged" runat="server" />


        </section>
        <%if (Session["FiltroAvanzadoActivo"] != null)%>
        <%{%>
        <%if ((bool)Session["FiltroAvanzadoActivo"])%>

        <% {%>
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <label for="<%= dDownListCampo.ClientID %>" style="text-align: center">Campo</label>
                <asp:DropDownList ID="dDownListCampo" CssClass="filtro_campo" OnSelectedIndexChanged="dDownListCampo_SelectedIndexChanged" AutoPostBack="true" runat="server">
                    <asp:ListItem>Código</asp:ListItem>
                    <asp:ListItem>Modelo</asp:ListItem>
                    <asp:ListItem>Precio</asp:ListItem>
                </asp:DropDownList>


                <%if (Session["validarListaIdPrecio"] != null)%>
                <%{%>
                <%if ((bool)Session["validarListaIdPrecio"]) %>

                <% { %>

                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <label for="<%= dDownListCriterio.ClientID %>" style="text-align: center">Criterio</label>
                        <asp:DropDownList ID="dDownListCriterio" CssClass="filtro_criterio" runat="server">

                            <asp:ListItem>Mayor a</asp:ListItem>
                            <asp:ListItem>Menor a</asp:ListItem>
                            <asp:ListItem>Igual a</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <%}%>
                <%}

                %>

                <%if (Session["validarListaModelo"] != null)%>

                <%{%>
                <%if ((bool)Session["validarListaModelo"])%>

                <%{%>

                <asp:UpdatePanel runat="server">
                    <ContentTemplate>


                        <label for="<%= dDownListCriterio1.ClientID %>" style="text-align: center">Criterio</label>
                        <asp:DropDownList ID="dDownListCriterio1" CssClass="filtro_criterio" runat="server">

                            <asp:ListItem>Contiene la letra/palabra</asp:ListItem>
                            <asp:ListItem>Empieza con la letra/palabra</asp:ListItem>
                            <asp:ListItem>Termina con la letra/palabra</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%}%>
                <%}%>




                <label for="<%= tBoxBusqueda.ClientID %>" style="text-align: center">Búsqueda</label>
                <asp:TextBox ID="tBoxBusqueda" CssClass="filtro_busqueda" runat="server"></asp:TextBox>

                <asp:Button ID="btonFiltrar" CssClass="filtro_button" runat="server" Text="Buscar" OnClick="btonFiltrar_Click" />

            </ContentTemplate>
        </asp:UpdatePanel>

        <%}%>
        <%}%>
    </section>








    <asp:UpdatePanel ID="UpdatePanelCatalogo" runat="server">

        <ContentTemplate>


            <main class="catalogo">

                <asp:Repeater ID="repeaterProductosCatalogo" runat="server">
                    <ItemTemplate>


                        <article id="producto<%# Eval("Id")%>" class="catalogo_producto">



                            <article class="producto_informacion">

  

                                <span id="spanIconProductoFavorito">
                                    <asp:LinkButton ID="btonAgregarProductoFavorito" CssClass="fas fa-star"
                                        OnClick="btonAgregarProductoFavorito_Click" CommandArgument='<%# Eval("Id") %>'
                                        runat="server"
                                       Visible='<%# Session["listArticulosFavoritosId"] != null && !(((List<int>)Session["listArticulosFavoritosId"]).Contains(Convert.ToInt32(Eval("Id")))) %>'>
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="btonEliminarProductoFavorito" CssClass="fas fa-trash"
                                        OnClick="btonEliminarProductoFavorito_Click" CommandArgument='<%# Eval("Id") %>'
                                        runat="server"
                                        Visible='<%# Session["listArticulosFavoritosId"] != null && (((List<int>)Session["listArticulosFavoritosId"]).Contains(Convert.ToInt32(Eval("Id")))) %>'>
                                    </asp:LinkButton>

                                    <span id="iconProductoFavoritoDescripcionFuncionalidad" style="display: none; width: 90%"  >Producto Favorito</span>

                                    <span id="iconProductoEliminadoDescripcionFuncionalidad" style="display: none; width: 90%"  >Quitar Favorito</span>

                                </span>



                                <span class="informacion_id informacion_info"><%# Eval("CodigoArticulo") %></span>
                                <span>
                                    <p id="idnombreArticulo" class="informacion_nombre informacion_info"><%# Eval("Nombre") %></p>
                                </span>

                                <%//no es necesario el codigo unicode para el $ por compatibilidad ya que el IIS Expres convierte todo a HTML %>
                                <span class="informacion_precio informacion_info">
                                    <p id="idPrecio" style="display: inline-block"><%# Eval("Precio")%>$</p>
                                </span>

                                <div class="updatePanelVerDetalle">
                                    <asp:UpdatePanel ID="updatePanelVerDetalle" runat="server">
                                        <ContentTemplate>

                                            <span class="contenedor_efecto_aura">
                                                <asp:Button ID="btonDesplegarDetalleProducto" CssClass="informacion_button" runat="server" Text="Ver detalle" OnClick="btonDesplegarDetalleProducto_Click" CommandArgument='<%# Eval("Id")%>' CommandName="IdProducto" />
                                            </span>




                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>


                            </article>

                            <div class="producto_imagen">
                                <img src="<%# Eval("ImagenUrl") %>" class="imagen_img" alt="..." onerror="ImgAlt(this)">
                            </div>

                            <%--Imagen alternativa que se coloca en el elemeto image en el caso de que por algún motivo no cargue la imágen principal.--%>
                            <script>
                                function ImgAlt(imgElement) {

                                    imgElement.src = "../Imagenes/DefaultImage.png";
                                }
                            </script>


                        </article>




                    </ItemTemplate>
                </asp:Repeater>
            </main>
        </ContentTemplate>

    </asp:UpdatePanel>



    <script>
        window.onload = function () {
            // Verificar si existe un QueryString con 'producto'
            var urlParams = new URLSearchParams(window.location.search);
            var productoId = urlParams.get('producto');

            // Si existe el 'producto' en el QueryString
            if (productoId) {
                // Cambiar la URL para que tenga el formato deseado
                window.history.replaceState({}, "", "Home.aspx#producto" + productoId);

                // Realizar scroll al producto, si es necesario
                var elemento = document.querySelector("#producto" + productoId);
                if (elemento) {
                    elemento.scrollIntoView({ behavior: "smooth", block: "start" });
                }
            }
        };
</script>

    <script src="../Contenido_JS/Home.js"></script>

</asp:Content>
