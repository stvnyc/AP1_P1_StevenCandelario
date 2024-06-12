using AP1_P1_StevenCandelario.DAL;
using AP1_P1_StevenCandelario.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AP1_P1_StevenCandelario.Services;

public class ArticuloService(Contexto contexto)
{
	public async Task<bool> Existe(int ArticuloId)
	{
		return await contexto.Articulos
			.AnyAsync(a => a.ArticuloId == ArticuloId);
	}

	public async Task<bool> Existe(int articuloId, string descripcion)
	{
		return await contexto.Articulos
			.AnyAsync(a => a.ArticuloId != articuloId && a.Descripcion!.Equals(descripcion.ToLower()));
	}

	private async Task<bool> Insertar(Articulos articulo)
	{
		contexto.Articulos.Add(articulo);
		return await contexto.SaveChangesAsync() > 0;
	}

	private async Task<bool> Modificar(Articulos articulo)
	{
		contexto.Articulos.Update(articulo);
		var modificado = await contexto.SaveChangesAsync() > 0;
		contexto.Entry(articulo).State = EntityState.Modified;
		return modificado;
	}

	public async Task<bool> Guardar(Articulos articulo)
	{
		if (!await Existe(articulo.ArticuloId))
			return await Insertar(articulo);
		else
			return await Modificar(articulo);
	}

	public async Task<bool> Eliminar(int id)
	{
		var articulos = await contexto.Articulos
			.Where(a => a.ArticuloId == id)
			.ExecuteDeleteAsync();
		return articulos > 0;

	}

	public async Task<Articulos?> Buscar(int id)
	{
		return await contexto.Articulos
			.AsNoTracking()
			.FirstOrDefaultAsync(a => a.ArticuloId == id);
	}
	
	public async Task<List<Articulos>> Listar(Expression<Func<Articulos, bool>> criterio)
	{
		return await contexto.Articulos
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}
}
