# Desafio Full Stack Web Developer #

### Decisão da arquitetura utilizada

**Backend:**
- DDD (Domain Driven Design - Design Orientado a Domínio)

É uma abordagem de modelagem de software que segue um conjunto de práticas com objetivo de facilitar a implementação de regras/processos complexos de negócios, ao qual chamamos de domínio. Isolando entidades no domínio, temos a vantagem de implementar num único local da aplicação os motivos para a existência e a razão para a mudança dos dados (regras de negócio).
Por recomendação, podemos separar a solução em camadas de Apresentação, Aplicação, Domínio e Infra, porém, este procedimento não é uma regra. O importante é que as entidadees, os motivos para a existência e mudanças de estado destas entidades sejam facilmente identificadas no projeto.

[![DDD](/img/DDD.png "DDD")


- CQRS - Command and Query Responsibility Segregation (Segregação de Responsabilidade de Comando e Consulta)

Padrão que tem por objetivo separar a responsabilidade de escrita (commands) e leitura (queries) dos dados do sistema. Como vantagens na implementação desse padrão podemos citar a flexibilização na implementação de estratégias distintas para a escrita e leitura dos dados (um banco relacional para escrita e um banco desnormatizado para leitura, ORM para escrita e queries customizadas para o banco de leitura, utilização de cache, etc)

[![CQRS](/img/CQRS.png "CQRS")

**Frontend:**
Foi utilizada a versão 2 do Vue.js devido a compatibilidade com o pacote Vuetify. Por ter pouco conhecimento no desenvolvimento frontend, utilizei como base para a implementação a documentação nos sites [Vuejs.org][1] e [Vuetify.com][2]

### Lista de bibliotecas de terceiros utilizadas
**Backend:**
FluentValidation - para a validação dos dados de entrada
Mediatr - para auxiliar a implementação do padrão CQRS utilizado o pattern Mediator
Npgsql - para utilização do banco de dados PostgreSQL
Swagger - para documentação da API (Documentação da API em: [url da aplicação]/swagger/index.html)

**Frontend:**
Vuetify
Axios
V-Mask

### O que você melhoraria se tivesse mais tempo:
**Backend:**
-Rotinas de segurança para autenticação (JWT)
-Criação de um objeto de ValueObject para CPF, RA e Email
-Caso fosse um serviço real para produção e possuindo requisitos muito simples, utilizaria uma arquitetura menos sofisticada (API/Domain/Infra - Controller/Service/Repository)

**Frontend:**
-Separação do dialog de edição em um novo template
-Criação de componente específico para edição dos dados
-Criação de componente para alerta de validação dos dados e confirmação de exclusão

[1]: https://v2.vuejs.org/
[2]: https://vuetifyjs.com/
