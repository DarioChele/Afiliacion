using Afiliacion.Models.Page.PageElements;

namespace Afiliacion.Models.Page;
public class PageElementsModel{
    public List<EstadoAfiliado> lst_EstadoAfil { get; set; }
    public List<Genero> lst_Genero { get; set; }
    public List<Nacionalidad> lst_Nacionalidad { get; set; }
    public List<EstadoCivil> lst_EstadoCivil { get; set; }
    public List<Jornada> lst_Jornada { get; set; }
    public List<Operadora> lst_Operadora { get; set; }
    public Pais Pais{ get; set; }
    public List<Rural> lst_Rural { get; set; }
    public List<Titulo> lst_TituloProfesional { get; set; }
    public List<Cargo> lst_Cargo { get; set; }
    public List<Categoria> lst_Categoria { get; set; }
    public List<TipoContrato> lst_TipoContrato { get; set; }
    public List<AreaLaboral> lst_Area { get; set; }
    public List<ActividadEcono> lst_ActividadEconomica { get; set; }
    public List<OtrosIngresos> lst_OtrosIngresos { get; set; }
    public List<PersPoliExpuesta> lst_PersPoliExpuesta { get; set; }
    public List<NivelInstitucion> lst_NivelInstitucion { get; set; }
    public List<Sostenimiento> lst_Sostenimiento { get; set; }
    public List<Zona> lst_Zona { get; set; }
    public List<Banco> lst_Banco { get; set; }
    public List<TipoCuenta> lst_TipoCuenta { get; set; }
}
