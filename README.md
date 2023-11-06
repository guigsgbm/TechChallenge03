# Documenta��o do Projeto TechChallenge02

## Vis�o Geral
Este projeto � uma aplica��o .NET 6 que consiste em uma API e inclui v�rias etapas de configura��o, implanta��o e automa��o no Azure DevOps. O guia a seguir fornece instru��es detalhadas para configurar e implantar o projeto em um ambiente Azure.

## Requisitos
Antes de come�ar, certifique-se de ter o seguinte:
- Conta no Azure com as permiss�es necess�rias.
- Conta no Azure DevOps com acesso ao projeto desejado.
- .NET 6 SDK instalado localmente.
- Git instalado localmente.

## Passos para Configura��o e Implanta��o

### 1. Deploy dos recursos no Azure.
- Sql Server + Database.
- ACR (Azure Container Registry).
- App Service (Habilite o Application Insights).

### 2. Clone o Reposit�rio
- Abra um terminal e execute o seguinte comando para clonar o reposit�rio do GitHub:

  ```bash
  git clone https://github.com/guigsgbm/TechChallenge03.git

- Suba o reposit�rio em algum gerenciador de c�digo de sua prefer�ncia (Github, Azure DevOps etc..)

### 3. Configure as Pipelines no Azure DevOps

- No Azure DevOps, navegue at� o seu projeto.

- Ajuste os arquivos YAML conforme necess�rio para configurar a compila��o, teste e implanta��o de acordo com suas prefer�ncias.

### 4. Service Connections Azure DevOps

- Realize a configura��o de todas as conex�es utilizadas pelo Azure DevOps, isso pode incluir:

- ACR

- Subscription

- Sonar

- Reposit�rio

### 5. Configurar Vari�veis no Azure DevOps

- Dentro das pipelines YAML, verifique se as vari�veis necess�rias (se houver) est�o configuradas corretamente. Fa�a os ajustes necess�rios no arquivo YAML para incluir as vari�veis que seu projeto requer.

- Certifique-se de que todas as vari�veis de ambiente, segredos ou quaisquer outras configura��es espec�ficas do seu projeto estejam definidas nas pipelines do Azure DevOps de acordo com suas necessidades.

- Lembre-se de verificar se as pipelines tem permiss�o para acessar os grupos de vari�veis (Library).

### 6. Rodar as Pipelines

- Execute as pipelines no Azure DevOps para compilar, testar e implantar.

- ### Conclus�o

- Ao final, se todas pipelines rodarem com sucesso, o aplicativo ser� implantado, assim como todo o resto do fluxo ter� sido executado (testes, sonarcloud etc...)