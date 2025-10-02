Este repositório contém o desenvolvimento das atividades 7, 8 e 9 da "Atividade Única: Associações" para a disciplina de Programação Orientada a Objetos (POO) no semestre 2025/2. O foco é na criação de mini-aplicações web visuais e interativas (client-side, sem backend) para explorar conceitos como qualificadores, refatoração de maus cheiros e um quiz interativo sobre associações entre classes.Cada atividade inclui:Uma interface visual interativa em HTML/CSS/JavaScript.
Funcionalidades de exportação em PNG (para diagramas/canvas) e JSON/CSV (para dados e decisões).
Validação argumentativa baseada em conceitos como multiplicidade, opcionalidade, navegabilidade mínima, classe de associação, composição vs. agregação, qualificadores, cheiros e refatorações.
10 situações práticas por atividade, organizadas por níveis de dificuldade (Elementar, Intermediário, Avançado), com ênfase em debate estruturado e reflexão crítica (sem gabarito fixo).

As mini-apps são projetadas para uso em workshops/seminários, promovendo análise textual, simulações, editores de grafos e votações locais.Estrutura do Repositórioactivity7-qualificadores/: Mini-app "Simulador de Qualificadores".index.html: Página principal da app.
styles.css: Estilos visuais.
script.js: Lógica interativa (definição de qualificadores, importação CSV, detecção de colisões, exportação JSON/PNG).
data/: Exemplos de datasets CSV fictícios para teste.
docs/: Descrição das 10 situações e checklist.

activity8-refatorando-maus-cheiros/: Mini-app "Caçador de Cheiros".index.html: Página principal da app.
styles.css: Estilos visuais.
script.js: Lógica interativa (importação JSON de modelos, lint automático, refatorações simuladas, comparação lado a lado, exportação JSON/PNG).
models/: Exemplos de modelos JSON para importação.
docs/: Descrição dos cheiros comuns, 10 desafios e checklist de refatorações.

activity9-quiz-interativo/: Mini-app "Quiz Verdadeiro ou Falso".index.html: Página principal da app.
styles.css: Estilos visuais.
script.js: Lógica interativa (apresentação de afirmações, votação local via localStorage, gráficos de barras, timer, exportação JSON/CSV).
data/: Lista das 10 afirmações para debate.
docs/: Instruções para defesa de posições e pontos de atenção.

assets/: Recursos compartilhados (imagens, ícones, bibliotecas como Chart.js para gráficos ou html2canvas para exportação PNG).
docs/: Documentação geral, incluindo visão geral do workshop, critérios de decisão e rubrica de avaliação.
README.md: Este arquivo.

Tecnologias UtilizadasHTML5/CSS3/JavaScript (ES6+): Base para as mini-apps client-side.
Bibliotecas Opcionais:html2canvas: Para exportação de canvas como PNG.
Chart.js: Para gráficos em votações (Atividade 9).
PapaParse: Para parsing de CSV (Atividade 7).
LocalStorage: Para armazenamento local de sessões/votações.

Sem dependências de backend ou frameworks pesados (ex: React/Vue) para manter simplicidade e portabilidade.

Instruções de ExecuçãoClonar o Repositório:

git clone https://github.com/seu-usuario/poo-associacoes-atividades-7-8-9.git

Abrir Localmente:Abra o arquivo index.html de cada pasta de atividade em um navegador moderno (Chrome, Firefox, etc.).
Não requer servidor; roda diretamente via file:// (mas para features como drag-and-drop, use um servidor local simples).

Servidor Local (Opcional):Use ferramentas como Live Server (VS Code extension) ou npx http-server para simular um ambiente web real.

npm install -g http-server
http-server .

Acesse via http://localhost:8080/activity7-qualificadores/index.html (e аналогично para as outras).

Testes e Exportação:Cada app inclui botões para exportar resultados (PNG para visuais, JSON/CSV para dados).
Teste com os dados de exemplo fornecidos.

Contribuições e NotasEste projeto é para fins acadêmicos e segue as diretrizes do material do PDF fornecido.


LicençaMIT License - Livre para uso educacional.

