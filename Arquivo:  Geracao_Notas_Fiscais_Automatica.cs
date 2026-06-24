using System;
using RAP10.Configuracoes;

namespace RAP10.ModuloLojistas
{
    public class GeracaoNotasFiscais
    {
        private readonly ConfiguracoesSistema _config;

        public GeracaoNotasFiscais()
        {
            _config = ConfiguracoesSistema.Carregar();
        }

        public string GerarNotaAutomatica(Pedido pedido, Loja loja)
        {
            // Calcula impostos e valores automaticamente
            decimal aliquotaICMS = _config.Impostos.ICMS;
            decimal valorImposto = Math.Round(pedido.ValorTotal * (aliquotaICMS / 100), 2);
            decimal valorLiquido = pedido.ValorTotal - valorImposto;

            // Envia para a SEFAZ e obtém número da nota
            string numeroNota = IntegracaoSefaz.Enviar(pedido, loja, valorImposto);

            // Registra no sistema e envia ao cliente
            Log.Registrar($"Nota {numeroNota} gerada para pedido {pedido.Id}");
            Notificacao.Enviar(pedido.Cliente, $"Sua nota fiscal foi emitida! Acesse: {_config.LinkNotaBase}/{numeroNota}");

            return numeroNota;
        }
    }
}
