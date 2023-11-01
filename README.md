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

### 1. Deploy do Azure SQL Server e Banco de Dados
- Faça login no portal Azure (https://portal.azure.com).
- Crie um novo servidor SQL e um banco de dados no Azure.
- Anote as informações de conexão, como a cadeia de conexão e as credenciais, pois serão necessárias posteriormente.
- Libere os IP's necessários no Firewall antes de realizar o deploy da aplicação

### 2. Deploy do Azure Container Registry (ACR)
- No portal Azure, crie um registro de contêineres.
- Anote o nome do registro e as credenciais de acesso.
- Habilite a opção "Admin User" na seção "Access keys"

### 3. Clone o Repositório
- Abra um terminal e execute o seguinte comando para clonar o repositório do GitHub:

  ```bash
  git clone https://github.com/guigsgbm/TechChallenge02.git

- Suba o repositório em algum gerenciador de código de sua preferência (Github, Azure DevOps etc..)

### 4. Configure as Pipelines no Azure DevOps

- No Azure DevOps, navegue até o seu projeto.

- Crie três pipelines YAML: uma para a API, outra para o WebApp e outra para o Banco de Dados. Use os arquivos `.yaml` fornecidos no repositório como ponto de partida.

- Ajuste os arquivos YAML conforme necessário para configurar a compilação, teste e implantação de acordo com suas preferências.

- Crie uma Service Connection **Docker Registry** entre o Azure DevOps e o Azure Container Registry.

### 5. Configurar Variáveis no Azure DevOps

- Dentro das pipelines YAML, verifique se as variáveis necessárias (se houver) estão configuradas corretamente. Faça os ajustes necessários no arquivo YAML para incluir as variáveis que seu projeto requer.

- Certifique-se de que todas as variáveis de ambiente, segredos ou quaisquer outras configurações específicas do seu projeto estejam definidas nas pipelines do Azure DevOps de acordo com suas necessidades.

- Lembre-se de verificar se as pipelines tem permissão para acessar os grupos de variáveis (Library).

### 6. Rodar as Pipelines

- Execute as pipelines no Azure DevOps para compilar, testar e implantar o Banco de dados, a API e o WebApp **(a de banco de dados precisa rodas antes das demais, para criar a estrutura do banco)**.

### 7. Deploy do Azure Container Instance

- Após a conclusão bem-sucedida das pipelines, você terá uma imagem do contêiner no ACR e as tabelas no Azure Database criadas.

- No portal Azure, crie duas instâncias de contêiners (ACI) e configure-as para usar as imagens dos contêiners criadas anteriormente, webapi e webapp (especificamente a tag "latest").

- ### Observações

- As pipelines foram configuradas com o seguinte comportamento: Triggers em pastas específicas acionam pipelines específicas, sempre que uma nova imagem for implantada no ACR, o Container Instance irá ser reinicializado automaticamente via pipeline para atualizar a imagem (por isso utilizar a tag latest).

- No momento que a aplicação é inicializada, um usuário Administrador é criado no banco de dados atráves da classe "Shared/Data/DbInitializer.cs".

- Para registrar usuários "Adm" basta realizar uma chamada post para a API no endpoint "api/account/register", utilizando a conta de adm padrão.