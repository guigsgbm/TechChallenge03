# Documentação do Projeto TechChallenge02

## Visão Geral
Este projeto é uma aplicação .NET 6 que consiste em uma API e inclui várias etapas de configuração, implantação e automação no Azure DevOps. O guia a seguir fornece instruções detalhadas para configurar e implantar o projeto em um ambiente Azure.

## Requisitos
Antes de começar, certifique-se de ter o seguinte:
- Conta no Azure com as permissões necessárias.
- Conta no Azure DevOps com acesso ao projeto desejado.
- .NET 6 SDK instalado localmente.
- Git instalado localmente.

## Passos para Configuração e Implantação

### 1. Deploy dos recursos no Azure.
- Sql Server + Database.
- ACR (Azure Container Registry).
- App Service (Habilite o Application Insights).

### 2. Clone o Repositório
- Abra um terminal e execute o seguinte comando para clonar o repositório do GitHub:

  ```bash
  git clone https://github.com/guigsgbm/TechChallenge03.git

- Suba o repositório em algum gerenciador de código de sua preferência (Github, Azure DevOps etc..)

### 3. Configure as Pipelines no Azure DevOps

- No Azure DevOps, navegue até o seu projeto.

- Ajuste os arquivos YAML conforme necessário para configurar a compilação, teste e implantação de acordo com suas preferências.

### 4. Service Connections Azure DevOps

- Realize a configuração de todas as conexões utilizadas pelo Azure DevOps, isso pode incluir:

- ACR

- Subscription

- Sonar

- Repositório

### 5. Configurar Variáveis no Azure DevOps

- Dentro das pipelines YAML, verifique se as variáveis necessárias (se houver) estão configuradas corretamente. Faça os ajustes necessários no arquivo YAML para incluir as variáveis que seu projeto requer.

- Certifique-se de que todas as variáveis de ambiente, segredos ou quaisquer outras configurações específicas do seu projeto estejam definidas nas pipelines do Azure DevOps de acordo com suas necessidades.

- Lembre-se de verificar se as pipelines tem permissão para acessar os grupos de variáveis (Library).

### 6. Rodar as Pipelines

- Execute as pipelines no Azure DevOps para compilar, testar e implantar.

- ### Conclusão

- Ao final, se todas pipelines rodarem com sucesso, o aplicativo será implantado, assim como todo o resto do fluxo terá sido executado (testes, sonarcloud etc...)