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

### 1. Deploy do Azure SQL Server e Banco de Dados
- Fa�a login no portal Azure (https://portal.azure.com).
- Crie um novo servidor SQL e um banco de dados no Azure.
- Anote as informa��es de conex�o, como a cadeia de conex�o e as credenciais, pois ser�o necess�rias posteriormente.
- Libere os IP's necess�rios no Firewall antes de realizar o deploy da aplica��o

### 2. Deploy do Azure Container Registry (ACR)
- No portal Azure, crie um registro de cont�ineres.
- Anote o nome do registro e as credenciais de acesso.
- Habilite a op��o "Admin User" na se��o "Access keys"

### 3. Clone o Reposit�rio
- Abra um terminal e execute o seguinte comando para clonar o reposit�rio do GitHub:

  ```bash
  git clone https://github.com/guigsgbm/TechChallenge02.git

- Suba o reposit�rio em algum gerenciador de c�digo de sua prefer�ncia (Github, Azure DevOps etc..)

### 4. Configure as Pipelines no Azure DevOps

- No Azure DevOps, navegue at� o seu projeto.

- Crie tr�s pipelines YAML: uma para a API, outra para o WebApp e outra para o Banco de Dados. Use os arquivos `.yaml` fornecidos no reposit�rio como ponto de partida.

- Ajuste os arquivos YAML conforme necess�rio para configurar a compila��o, teste e implanta��o de acordo com suas prefer�ncias.

- Crie uma Service Connection **Docker Registry** entre o Azure DevOps e o Azure Container Registry.

### 5. Configurar Vari�veis no Azure DevOps

- Dentro das pipelines YAML, verifique se as vari�veis necess�rias (se houver) est�o configuradas corretamente. Fa�a os ajustes necess�rios no arquivo YAML para incluir as vari�veis que seu projeto requer.

- Certifique-se de que todas as vari�veis de ambiente, segredos ou quaisquer outras configura��es espec�ficas do seu projeto estejam definidas nas pipelines do Azure DevOps de acordo com suas necessidades.

- Lembre-se de verificar se as pipelines tem permiss�o para acessar os grupos de vari�veis (Library).

### 6. Rodar as Pipelines

- Execute as pipelines no Azure DevOps para compilar, testar e implantar o Banco de dados, a API e o WebApp **(a de banco de dados precisa rodas antes das demais, para criar a estrutura do banco)**.

### 7. Deploy do Azure Container Instance

- Ap�s a conclus�o bem-sucedida das pipelines, voc� ter� uma imagem do cont�iner no ACR e as tabelas no Azure Database criadas.

- No portal Azure, crie duas inst�ncias de cont�iners (ACI) e configure-as para usar as imagens dos cont�iners criadas anteriormente, webapi e webapp (especificamente a tag "latest").

- ### Observa��es

- As pipelines foram configuradas com o seguinte comportamento: Triggers em pastas espec�ficas acionam pipelines espec�ficas, sempre que uma nova imagem for implantada no ACR, o Container Instance ir� ser reinicializado automaticamente via pipeline para atualizar a imagem (por isso utilizar a tag latest).

- No momento que a aplica��o � inicializada, um usu�rio Administrador � criado no banco de dados atr�ves da classe "Shared/Data/DbInitializer.cs".

- Para registrar usu�rios "Adm" basta realizar uma chamada post para a API no endpoint "api/account/register", utilizando a conta de adm padr�o.