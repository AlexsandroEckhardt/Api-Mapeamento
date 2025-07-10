using NDD.Space.Base.Domain.Resposta;

using System.Reflection;

namespace NDD.Api.Mapeamento.Domain
{
    public static class CodigosDeRetornoBase
    {
        /// <summary>
        /// "Requisicao processada com sucesso"
        /// </summary>
        public readonly static Tuple<int, string, TipoResposta> Info100 = Tuple.Create(100, "Requisicao processada com sucesso", TipoResposta.Informacao);

        /// <summary>
        /// Ocorreu um erro inesperado. Erro: {0}
        /// </summary>
        public readonly static Tuple<int, string, TipoResposta> Erro999 = Tuple.Create(999, "Ocorreu um erro inesperado. Erro: {0}", TipoResposta.Erro);

        public static RespostaDeRequisicao CriarResposta(Tuple<int, string, TipoResposta> tuple, params object[] parametros)
        {
            return new RespostaDeRequisicao()
            {
                Codigo = tuple.Item1,
                Mensagem = parametros == null ? tuple.Item2 : string.Format(tuple.Item2, parametros),
                TipoResposta = tuple.Item3
            };
        }

        public static bool RespostaDeRequisicaoIgual(Tuple<int, string, TipoResposta> tuple, RespostaDeRequisicao resposta)
        {
            return resposta.Codigo == tuple.Item1 &&
                   resposta.TipoResposta == tuple.Item3;
        }

        public static Tuple<int, string, TipoResposta> BuscarPorCodigo(int codigo, Type codigosDeRetornoBase)
        {
            List<Tuple<int, string, TipoResposta>> lista = new List<Tuple<int, string, TipoResposta>>();

            FieldInfo[] itens = codigosDeRetornoBase.GetFields(BindingFlags.Static | BindingFlags.Public);

            Array.ForEach(itens, item =>
            {
                if (item.FieldType.IsAssignableFrom(typeof(Tuple<int, string, TipoResposta>)))
                {
                    Tuple<int, string, TipoResposta> value = (Tuple<int, string, TipoResposta>)item.GetValue(null);

                    lista.Add(value);
                }
            });

            return lista.FirstOrDefault(tupla => tupla.Item1 == codigo);
        }
    }

    public static class TupleExtensions
    {
        public static int Codigo(this Tuple<int, string, TipoResposta> tuple)
        {
            return tuple.Item1;
        }
        public static string Mensagem(this Tuple<int, string, TipoResposta> tuple)
        {
            return tuple.Item2;
        }
        public static TipoResposta Tipo(this Tuple<int, string, TipoResposta> tuple)
        {
            return tuple.Item3;
        }

        public static RespostaDeRequisicao CriarResposta(this Tuple<int, string, TipoResposta> tuple)
        {
            return tuple.CriarResposta(null);
        }

        public static RespostaDeRequisicao CriarResposta(this Tuple<int, string, TipoResposta> tuple, params object[] parametros)
        {
            return new RespostaDeRequisicao()
            {
                Codigo = tuple.Item1,
                Mensagem = parametros == null ? tuple.Item2 : string.Format(tuple.Item2, parametros),
                TipoResposta = tuple.Item3
            };
        }

        public static bool RespostaDeRequisicaoIgual(this Tuple<int, string, TipoResposta> tuple, RespostaDeRequisicao resposta)
        {
            return resposta.Codigo == tuple.Item1 &&
                   resposta.TipoResposta == tuple.Item3;
        }

        public static bool RespostaDeRequisicaoIgual(this RespostaDeRequisicao resposta, Tuple<int, string, TipoResposta> tuple)
        {
            return resposta.Codigo == tuple.Item1 &&
                   resposta.Mensagem == tuple.Item2 &&
                   resposta.TipoResposta == tuple.Item3;
        }

        public static bool RespostaDeRequisicaoIgual(this RespostaDeRequisicao resposta, RespostaDeRequisicao respostaParaComparar)
        {
            return resposta.Codigo == respostaParaComparar.Codigo &&
                   resposta.Mensagem == respostaParaComparar.Mensagem &&
                   resposta.TipoResposta == respostaParaComparar.TipoResposta;
        }
    }
}