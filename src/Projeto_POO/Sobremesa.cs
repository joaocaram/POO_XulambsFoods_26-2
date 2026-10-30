using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods {
    public class Sobremesa : Produto{
        private ESobremesa _tipo;

        public Sobremesa(ESobremesa tipo) {
            _tipo = tipo;
        }

        public double CalcularValorFinal() {
            return _tipo.Valor();
        }

        public override bool Equals(object? obj) {
            Sobremesa outra = obj as Sobremesa;
            return outra?._tipo.Equals(this._tipo) ?? false;
        }

        public override int GetHashCode() {
            return _tipo.GetHashCode();
        }

        public override string ToString() {
            return $"{_tipo.Nome()}\n\tVALOR: {_tipo.Valor():C2}\n================";
        }
    }
}
