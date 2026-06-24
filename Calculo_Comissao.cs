using System;

namespace RAP10.ModuloCorridas
{
    public class CalculoComissao
    {
        public static (decimal ValorParceiro, decimal ValorPlataforma) Calcular(decimal ValorTotal, string TipoServico)
        {
            decimal taxa = TipoServico.ToLower() switch
            {
                "carro" => 0.20m,
                "moto" => 0.18m,
                "motoboy" => 0.15m,
                "carga" => 0.12m,
                _ => 0.15m
            };

            decimal comissao = Math.Round(ValorTotal * taxa, 2);
            decimal liquido = Math.Round(ValorTotal - comissao, 2);

            return (liquido, comissao);
        }
    }
}
