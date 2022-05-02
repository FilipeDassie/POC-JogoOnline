Olá, seja bem-vindo(a) a documentação do projeto <b>JogoOnline</b>!
<br><br>
O mesmo foi desenvolvido como uma <i>API .Net Core 3.1</i> utilizando como IDE o VS 2019.
<br><br>
Abaixo estão descritos os procedimentos para a correta execução do projeto. Por gentileza, seguir atentamente todos os passos abaixo:
<br><br>
1º passo: clonar o projeto em um diretório local de sua preferência;
<br>
2º passo: compilar o projeto (verifique se todas as dependências foram baixadas);
<br>
3º passo: descompactar o arquivo "Files\Redis.rar" em um diretório e rodar o arquivo "<i>redis-server.exe</i>" para startar o servidor do Redis;
<br>
4º passo: abrir o arquivo "<i>appsettings.json</i>" e realizar a configuração das chaves "<i>ConnectionStrings</i>" e "<i>AppSettings</i>";
<br>
5º passo: iniciar o projeto. a API irá direcionar para a aplicação do swagger como padrão;
<br>
6º passo: verificar se a base de dados (SGBD) foi criada com sucesso.
<br>

Abaixo as url's que fazem parte deste projeto:
<br>
http://localhost:48734/swagger: aplicação responsável pela exposição dos endpoints da API;
<br>
http://localhost:48734/hangfire: aplicação responsável pela exibição dos jobs (processamento do ranking dos jogadores).
<br><br>
<b>Aviso</b>: Tomei a liberdade para desenvolver algumas coisas de maneira diferente do solicitado, bem como algumas funcionalidades a mais. Podemos discutir estas questões posteriormente.
<br><br>
Qualquer dúvida estarei à disposição para ajudar.
<br><br>
Contatos:
<br>
Email: fdolipi@gmail.com
<br>
Whats: +5541996826379
