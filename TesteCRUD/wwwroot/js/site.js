const campoCep = document.getElementById("enderecoModel_cep");
const campoCPF = document.getElementById("CPF");


if (campoCep != null) {
   campoCep.addEventListener("change", (event) => {
      onchange = (event) => {
         PreencheEndereco();
      }
   })
}


const PreencheEndereco = async () => {
   var validacep = /^[0-9]{8}$/;
   if (campoCep.value.includes("-")) campoCep.value = campoCep.value.replace("-", "")
   if (validacep.test(campoCep.value) && campoCep.value.length == 8) {
      const url = `https://viacep.com.br/ws/${campoCep.value}/json/`;
      const dadosFetch = await fetch(url);
      const dados = await dadosFetch.json();
      if (!dados.hasOwnProperty('erro')) return PreencherCamposEndereco(dados);
   }
   LimpaCamposCEP()
}

const PreencherCamposEndereco = dado => {
   if (!campoCep.value.includes("-")) campoCep.value = `${campoCep.value.slice(0, 5)}-${campoCep.value.slice(5)}`
   document.getElementById('enderecoModel_endereco').value = `${dado.logradouro} - ${dado.bairro}`;
   document.getElementById('enderecoModel_cidade').value = (dado.localidade);
   document.getElementById('enderecoModel_estado').value = (dado.uf);
}

const LimpaCamposCEP = () => {
   window.alert('CEP Inválido!');
   document.getElementById('enderecoModel_endereco').value = '';
   document.getElementById('enderecoModel_cidade').value = '';
   document.getElementById('enderecoModel_estado').value = '';
}
const LimpaCamposCPF = () => {
   alert('CPF Inválido!!')
   campoCPF.value = '';
}


campoCPF.addEventListener("change", (event) => {
   onchange = (event) => {
      TestaCPF();
   }
})

const TestaCPF = () => {
   var validacpf = /^[0-9]{11}$/;
   var Soma;
   var Resto;
   Soma = 0;
   if (campoCPF.value.includes(".")) campoCPF.value = campoCPF.value.replace(/\./g, "");
   if (campoCPF.value.includes("-")) campoCPF.value = campoCPF.value.replace("-", "");
   if (campoCPF.value.includes(" ")) campoCPF.value = campoCPF.value.replace(/\s/g, "");
   if (!validacpf.test(campoCPF.value) || campoCPF.value == '00000000000') return LimpaCamposCPF();

   for (i = 1; i <= 9; i++) Soma = Soma + parseInt(campoCPF.value.substring(i - 1, i)) * (11 - i);
   Resto = (Soma * 10) % 11;

   if ((Resto == 10) || (Resto == 11)) Resto = 0;
   if (Resto != parseInt(campoCPF.value.substring(9, 10))) return LimpaCamposCPF();

   Soma = 0;
   for (i = 1; i <= 10; i++) Soma = Soma + parseInt(campoCPF.value.substring(i - 1, i)) * (12 - i);
   Resto = (Soma * 10) % 11;

   if ((Resto == 10) || (Resto == 11)) Resto = 0;
   if (Resto != parseInt(campoCPF.value.substring(10, 11))) return LimpaCamposCPF();
   if (!campoCPF.value.includes("-") || !campoCPF.value.includes(".")) return campoCPF.value = `${campoCPF.value.slice(0, 3)}.${campoCPF.value.slice(3, 6)}.${campoCPF.value.slice(6, 9)}-${campoCPF.value.slice(9, 14)}`
}