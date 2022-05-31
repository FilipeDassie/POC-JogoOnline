Olá, seja bem-vindo(a)!<br><br>
<b>1. Escopo</b><br><br>
Você está trabalhando para uma empresa de jogos online que opera vários servidores de jogos. Cada jogo resulta em ganho ou perda de pontos para o jogador. Os dados são mantidos em memória por cada servidor e periodicamente esses dados são persistidos. Sua tarefa é implementar um serviço que exponha 2 endpoints:<br><br>
<i>Endpoint 1:</i><br><br>
Permite que os servidores persistam os dados do resultado de um jogo:<br><br>
•	PlayerId (string) - ID do jogador<br>
•	GameId (string) - ID do jogo<br>
•	Win (long) – o número de pontos ganhos (positivos ou negativos)<br>
•	Timestamp (date time) – data de quando o jogo foi realizado (UTC)<br><br>
Como resultado da chamada a esse endpoint o balanço dos pontos do jogador devem ser primeiramente armazenado em memória no servidor e após um determinado tempo deverá persistir todas as informações que estão em memória para o banco de dados de uma só vez. Esse tempo deverá ser facilmente configurável pelo usuário que vai implantar a solução, pois ainda não se sabe ainda qual será a volumetria de informações e as especificações do servidor. Se um jogador não tem um registro do balanço dos seus pontos no banco de dados, ele deverá ser criado. NOTA: Um grupo de dados pode conter diversos registros de um único jogador (i.e. o jogador participou em vários jogos). Existem múltiplos servidores de jogos, realizando partidas simultâneas de jogos diferentes, então o serviço irá receber várias requisições concorrentes, que podem conter resultados do mesmo jogador. Inicialmente este serviço irá rodar como um piloto em um único servidor. Portanto, dados perdidos devido ao mal funcionamento do servidor ou do serviço não é considerado crítico, mas não deveria ocorrer dentro de circunstâncias normais.<br><br>
<i>Endpoint 2:</i><br><br>
Esse endpoint permite que os web sites, onde o jogador inicia os jogos, mostre um placar da classificação dos 100 melhores jogadores. Os 100 melhores jogadores são ordenados pelo balanço de pontos que eles possuem em ordem descendente. Ele retornará os seguintes dados:<br><br>
•	PlayerId (string) – ID do jogador<br>
•	Balance (long) – balanço de pontos do jogador<br>
•	LastUpdateDate (date time) – data em que o balanço de pontos do jogador foi atualizado pela última vez (usando o fuso horário do servidor de aplicação)<br><br>
<b>2. Tecnologias usadas do projeto:</b><br><br>
=> Net Core 3.1;<br>
=> AutoMapper 9.0;<br>
=> Hangfire 1.7.28;<br>
=> Entity Framework Core 2.2.4;<br>
=> Naylah 2020.8.30;<br>
=> Redis.<br><br>
<b>3. Procedimentos</b><br><br>
Abaixo estão descritos os procedimentos para a correta execução do projeto. Por gentileza, seguir atentamente todos os passos abaixo:<br><br>
1º passo: clonar o projeto em um diretório local de sua preferência;<br>
2º passo: compilar o projeto (verifique se todas as dependências foram baixadas);<br>
3º passo: descompactar o arquivo "Files\Redis.rar" em um diretório e rodar o arquivo "<i>redis-server.exe</i>" para startar o servidor do Redis;<br>
4º passo: abrir o arquivo "<i>appsettings.json</i>" e realizar a configuração das chaves "<i>ConnectionStrings</i>" e "<i>AppSettings</i>";<br>
5º passo: iniciar o projeto. a API irá direcionar para a aplicação do swagger como padrão;<br>
6º passo: verificar se a base de dados (SGBD) foi criada com sucesso.<br><br>
Abaixo as url's que fazem parte deste projeto:<br>
http://localhost:48734/swagger: aplicação responsável pela exposição dos endpoints da API;<br>
http://localhost:48734/hangfire: aplicação responsável pela exibição dos jobs (processamento do ranking dos jogadores).<br><br>
Contatos:<br>
Email: fdolipi@gmail.com<br>
Whats: +55 41 996826379
