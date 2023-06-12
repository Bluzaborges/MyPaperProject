DROP TABLE IF EXISTS logs;
CREATE TABLE IF NOT EXISTS logs (
	id SERIAL PRIMARY KEY,
	creation_date DATE,
	type VARCHAR(10),
	message TEXT
);

DROP TABLE IF EXISTS areas;
CREATE TABLE IF NOT EXISTS areas (
	id SERIAL PRIMARY KEY,
	name VARCHAR(100) NOT NULL
);

DROP TABLE IF EXISTS subareas;
CREATE TABLE IF NOT EXISTS subareas (
	id SERIAL PRIMARY KEY,
	id_area INT REFERENCES areas,
	name VARCHAR(100) NOT NULL
);

-- Inserindo dados na tabela 'areas'
INSERT INTO areas (name) VALUES ('Matemática');

-- Inserindo dados na tabela 'subareas'
INSERT INTO subareas (id_area, name) VALUES (1, 'Álgebra');
INSERT INTO subareas (id_area, name) VALUES (1, 'Conjuntos');
INSERT INTO subareas (id_area, name) VALUES (1, 'Lógica matemática');
INSERT INTO subareas (id_area, name) VALUES (1, 'Teoria dos números');
INSERT INTO subareas (id_area, name) VALUES (1, 'Grupo de álgebra não-comutativa');
INSERT INTO subareas (id_area, name) VALUES (1, 'Álgebra comutativa');
INSERT INTO subareas (id_area, name) VALUES (1, 'Geometria algébrica');
INSERT INTO subareas (id_area, name) VALUES (1, 'Análise');
INSERT INTO subareas (id_area, name) VALUES (1, 'Análise complexa');
INSERT INTO subareas (id_area, name) VALUES (1, 'Análise funcional');
INSERT INTO subareas (id_area, name) VALUES (1, 'Análise funcional não-linear');
INSERT INTO subareas (id_area, name) VALUES (1, 'Equações diferenciais ordinárias');
INSERT INTO subareas (id_area, name) VALUES (1, 'Equações diferenciais parciais');
INSERT INTO subareas (id_area, name) VALUES (1, 'Equações diferenciais funcionais');
INSERT INTO subareas (id_area, name) VALUES (1, 'Geometria e topologia');
INSERT INTO subareas (id_area, name) VALUES (1, 'Geometria diferencial');
INSERT INTO subareas (id_area, name) VALUES (1, 'Topologia algébrica');
INSERT INTO subareas (id_area, name) VALUES (1, 'Topologia das variedades');
INSERT INTO subareas (id_area, name) VALUES (1, 'Sistemas dinâmicos');
INSERT INTO subareas (id_area, name) VALUES (1, 'Teoria das singularidades e teoria das catástrofes');
INSERT INTO subareas (id_area, name) VALUES (1, 'Teoria das folheações');
INSERT INTO subareas (id_area, name) VALUES (1, 'Matemática aplicada');
INSERT INTO subareas (id_area, name) VALUES (1, 'Física matemática');
INSERT INTO subareas (id_area, name) VALUES (1, 'Análise numérica');
INSERT INTO subareas (id_area, name) VALUES (1, 'Matemática discreta e combinatória');

-- Inserindo dados na tabela 'areas'
INSERT INTO areas (name) VALUES ('Probabilidade e Estatística');

-- Inserindo dados na tabela 'subareas'
INSERT INTO subareas (id_area, name) VALUES (2, 'Teoria Geral e Fundamentos da Probabilidade');
INSERT INTO subareas (id_area, name) VALUES (2, 'Teoria Geral e Processos Estocásticos');
INSERT INTO subareas (id_area, name) VALUES (2, 'Teoremas de Limite');
INSERT INTO subareas (id_area, name) VALUES (2, 'Processos Markovianos');
INSERT INTO subareas (id_area, name) VALUES (2, 'Análise Estocástica');
INSERT INTO subareas (id_area, name) VALUES (2, 'Processos Estocásticos Especiais');
INSERT INTO subareas (id_area, name) VALUES (2, 'Estatística');
INSERT INTO subareas (id_area, name) VALUES (2, 'Fundamentos da Estatística');
INSERT INTO subareas (id_area, name) VALUES (2, 'Inferência Paramétrica');
INSERT INTO subareas (id_area, name) VALUES (2, 'Inferência Não-Paramétrica');
INSERT INTO subareas (id_area, name) VALUES (2, 'Inferência em Processos Estocásticos');
INSERT INTO subareas (id_area, name) VALUES (2, 'Análise Multivariada');
INSERT INTO subareas (id_area, name) VALUES (2, 'Regressão e Correlação');
INSERT INTO subareas (id_area, name) VALUES (2, 'Planejamento de Experimentos');
INSERT INTO subareas (id_area, name) VALUES (2, 'Análise de Dados');
INSERT INTO subareas (id_area, name) VALUES (2, 'Probabilidade e Estatística Aplicadas');

-- Inserindo dados na tabela 'areas'
INSERT INTO areas (name) VALUES ('Ciência da Computação');
INSERT INTO areas (name) VALUES ('Astronomia');

-- Inserindo dados na tabela 'subareas'
INSERT INTO subareas (id_area, name) VALUES (3, 'Teoria da Computação');
INSERT INTO subareas (id_area, name) VALUES (3, 'Computabilidade e Modelos de Computação');
INSERT INTO subareas (id_area, name) VALUES (3, 'Linguagens Formais e Autômatos');
INSERT INTO subareas (id_area, name) VALUES (3, 'Análise de Algoritmos e Complexidade de Computação');
INSERT INTO subareas (id_area, name) VALUES (3, 'Lógicas e Semântica de Programas');
INSERT INTO subareas (id_area, name) VALUES (3, 'Matemática da Computação');
INSERT INTO subareas (id_area, name) VALUES (3, 'Matemática Simbólica');
INSERT INTO subareas (id_area, name) VALUES (3, 'Modelos Analíticos e de Simulação');
INSERT INTO subareas (id_area, name) VALUES (3, 'Metodologia e Técnicas da Computação');
INSERT INTO subareas (id_area, name) VALUES (3, 'Linguagens de Programação');
INSERT INTO subareas (id_area, name) VALUES (3, 'Engenharia de Software');
INSERT INTO subareas (id_area, name) VALUES (3, 'Banco de Dados');
INSERT INTO subareas (id_area, name) VALUES (3, 'Sistemas de Informação');
INSERT INTO subareas (id_area, name) VALUES (3, 'Processamento Gráfico (Graphics)');
INSERT INTO subareas (id_area, name) VALUES (3, 'Sistema de Computação');
INSERT INTO subareas (id_area, name) VALUES (3, 'Hardware');
INSERT INTO subareas (id_area, name) VALUES (3, 'Arquitetura de Sistemas de Computação');
INSERT INTO subareas (id_area, name) VALUES (3, 'Software Básico');
INSERT INTO subareas (id_area, name) VALUES (3, 'Teleinformática');

INSERT INTO subareas (id_area, name) VALUES (4, 'Astronomia');
INSERT INTO subareas (id_area, name) VALUES (4, 'Astronomia de Posição e Mecânica Celeste');
INSERT INTO subareas (id_area, name) VALUES (4, 'Astronomia Fundamental');
INSERT INTO subareas (id_area, name) VALUES (4, 'Astronomia Dinâmica');
INSERT INTO subareas (id_area, name) VALUES (4, 'Astrofísica Estelar');
INSERT INTO subareas (id_area, name) VALUES (4, 'Astrofísica do Meio Interestelar');
INSERT INTO subareas (id_area, name) VALUES (4, 'Meio Interestelar');
INSERT INTO subareas (id_area, name) VALUES (4, 'Nebulosa');
INSERT INTO subareas (id_area, name) VALUES (4, 'Astrofísica Extragaláctica');
INSERT INTO subareas (id_area, name) VALUES (4, 'Galáxias');
INSERT INTO subareas (id_area, name) VALUES (4, 'Aglomerados de Galáxias');
INSERT INTO subareas (id_area, name) VALUES (4, 'Quasares');
INSERT INTO subareas (id_area, name) VALUES (4, 'Cosmologia');
INSERT INTO subareas (id_area, name) VALUES (4, 'Astrofísica do Sistema Solar');
INSERT INTO subareas (id_area, name) VALUES (4, 'Física Solar');
INSERT INTO subareas (id_area, name) VALUES (4, 'Movimento da Terra');
INSERT INTO subareas (id_area, name) VALUES (4, 'Sistema Planetário');
INSERT INTO subareas (id_area, name) VALUES (4, 'Instrumentação Astronômica');
INSERT INTO subareas (id_area, name) VALUES (4, 'Astronomia Ótica');
INSERT INTO subareas (id_area, name) VALUES (4, 'Radioastronomia');
INSERT INTO subareas (id_area, name) VALUES (4, 'Astronomia Espacial');
INSERT INTO subareas (id_area, name) VALUES (4, 'Processamento de Dados Astronômicos');

-- Inserindo dados na tabela 'areas'
INSERT INTO areas (name) VALUES ('Física');

-- Inserindo dados na tabela 'subareas'
INSERT INTO subareas (id_area, name) VALUES (5, 'Física Geral');
INSERT INTO subareas (id_area, name) VALUES (5, 'Métodos Matemáticos da Física');
INSERT INTO subareas (id_area, name) VALUES (5, 'Física Clássica e Física Quântica; Mecânica e Campos');
INSERT INTO subareas (id_area, name) VALUES (5, 'Relatividade e Gravitação');
INSERT INTO subareas (id_area, name) VALUES (5, 'Física Estatística e Termodinâmica');
INSERT INTO subareas (id_area, name) VALUES (5, 'Metrologia, Tec. Ger. de Lab. e Sist. de Instrumentação');
INSERT INTO subareas (id_area, name) VALUES (5, 'Instrumentação Específica de Uso Geral em Física');
INSERT INTO subareas (id_area, name) VALUES (5, 'Áreas Clássicas de Fenomenologia e suas Aplicações');
INSERT INTO subareas (id_area, name) VALUES (5, 'Eletricidade e Magnetismo; Campos e Partículas Carregadas');
INSERT INTO subareas (id_area, name) VALUES (5, 'Ótica');
INSERT INTO subareas (id_area, name) VALUES (5, 'Acústica');
INSERT INTO subareas (id_area, name) VALUES (5, 'Transferência de Calor; Processos Térmicos e Termodinâmicos');
INSERT INTO subareas (id_area, name) VALUES (5, 'Mecânica, Elasticidade e Reologia');
INSERT INTO subareas (id_area, name) VALUES (5, 'Dinâmica dos Fluidos');
INSERT INTO subareas (id_area, name) VALUES (5, 'Física das Partículas Elementares e Campos');
INSERT INTO subareas (id_area, name) VALUES (5, 'Teoria Geral de Partículas e Campos');
INSERT INTO subareas (id_area, name) VALUES (5, 'Teor.Esp. e Mod.de Interação; Sist.de Partículas; R.Cósmicos');
INSERT INTO subareas (id_area, name) VALUES (5, 'Reações Específicas e Fenomenologia de Partículas');
INSERT INTO subareas (id_area, name) VALUES (5, 'Propriedades de Partículas Específicas e Ressonâncias');
INSERT INTO subareas (id_area, name) VALUES (5, 'Física Nuclear');
INSERT INTO subareas (id_area, name) VALUES (5, 'Estrutura Nuclear');
INSERT INTO subareas (id_area, name) VALUES (5, 'Desintegração Nuclear e Radioatividade');
INSERT INTO subareas (id_area, name) VALUES (5, 'Reações Nucleares e Espalhamento Geral');
INSERT INTO subareas (id_area, name) VALUES (5, 'Reações Nucleares e Espalhamento (Reações Específicas)');
INSERT INTO subareas (id_area, name) VALUES (5, 'Propriedades de Núcleos Específicos');
INSERT INTO subareas (id_area, name) VALUES (5, 'Met.Exper.e Instrum.Para Part.Element.e Física Nuclear');
INSERT INTO subareas (id_area, name) VALUES (5, 'Física Atômica e Molecular');
INSERT INTO subareas (id_area, name) VALUES (5, 'Estrutura Eletrônica de Átomos e Moléculas; Teoria');
INSERT INTO subareas (id_area, name) VALUES (5, 'Espectros Atômicos e Integração de Fótons');
INSERT INTO subareas (id_area, name) VALUES (5, 'Espectros Molec. e Interações de Fótons com Moléculas');
INSERT INTO subareas (id_area, name) VALUES (5, 'Processos de Colisão e Interações de Átomos e Moléculas');
INSERT INTO subareas (id_area, name) VALUES (5, 'Inf.Sob.Atom.e Mol.Obit.Experimentalmente; Inst.e Técnicas');
INSERT INTO subareas (id_area, name) VALUES (5, 'Estudos de Átomos e Moléculas Especiais');
INSERT INTO subareas (id_area, name) VALUES (5, 'Física dos Flúidos, Física de Plasmas e Descargas Elétricas');
INSERT INTO subareas (id_area, name) VALUES (5, 'Cinética e Teor.de Transp.de Flúidos; Propried.Fis.de Gases');
INSERT INTO subareas (id_area, name) VALUES (5, 'Física de Plasmas e Descargas Elétricas');
INSERT INTO subareas (id_area, name) VALUES (5, 'Física da Matéria Condensada');
INSERT INTO subareas (id_area, name) VALUES (5, 'Estrutura de Líquidos e Sólidos; Cristalografia');
INSERT INTO subareas (id_area, name) VALUES (5, 'Propriedades Mecânicas e Acústicas da Matéria Condensada');
INSERT INTO subareas (id_area, name) VALUES (5, 'Dinâmica da Rede e Estatística de Cristais');
INSERT INTO subareas (id_area, name) VALUES (5, 'Equação de Estado, Equilíb. de Fases e Transições de Fases');
INSERT INTO subareas (id_area, name) VALUES (5, 'Propriedades Térmicas da Matéria Condensada');
INSERT INTO subareas (id_area, name) VALUES (5, 'Propriedades de Transp.de Matéria Cond. (Não Eletrônicas)');
INSERT INTO subareas (id_area, name) VALUES (5, 'Campos Quânticos e Sólidos, Hélio, Líquido, Sólido');
INSERT INTO subareas (id_area, name) VALUES (5, 'Superfícies e Interfaces; Películas e Filamentos');
INSERT INTO subareas (id_area, name) VALUES (5, 'Estados Eletrônicos');
INSERT INTO subareas (id_area, name) VALUES (5, 'Transp.Eletr.e Propr.Elet.de Superfícies; Interf.e Películas');
INSERT INTO subareas (id_area, name) VALUES (5, 'Estrut.Eletr.e Propr.Elet.de Superfícies; Interf.e Películas');
INSERT INTO subareas (id_area, name) VALUES (5, 'Supercondutividade');
INSERT INTO subareas (id_area, name) VALUES (5, 'Materiais Magnéticos e Propriedades Magnéticas');
INSERT INTO subareas (id_area, name) VALUES (5, 'Ress.Magn. Rel.Mat.Cond.; Efeit.Mössbauer; Corr.Ang.Perturbada');
INSERT INTO subareas (id_area, name) VALUES (5, 'Materiais Dielétricos e Propriedades Dielétricas');
INSERT INTO subareas (id_area, name) VALUES (5, 'Prop.Otic.e Espec.Matr.Cond.; Outras Inter.Mat.com Rad.Part.');
INSERT INTO subareas (id_area, name) VALUES (5, 'Emissão Eletron.e Iônica por Liq.e Sólidos; Fenom.de Impacto');

-- Inserindo dados na tabela 'areas'
INSERT INTO areas (name) VALUES ('Química');

-- Inserindo dados na tabela 'subareas'
INSERT INTO subareas (id_area, name) VALUES (6, 'Química Orgânica');
INSERT INTO subareas (id_area, name) VALUES (6, 'Estrutura, Conformação e Estereoquímica');
INSERT INTO subareas (id_area, name) VALUES (6, 'Síntese Orgânica');
INSERT INTO subareas (id_area, name) VALUES (6, 'Físico-Química Orgânica');
INSERT INTO subareas (id_area, name) VALUES (6, 'Fotoquímica Orgânica');
INSERT INTO subareas (id_area, name) VALUES (6, 'Química dos Produtos Naturais');
INSERT INTO subareas (id_area, name) VALUES (6, 'Evolução, Sistemática e Ecologia Química');
INSERT INTO subareas (id_area, name) VALUES (6, 'Polímeros e Colóides');
INSERT INTO subareas (id_area, name) VALUES (6, 'Química Inorgânica');
INSERT INTO subareas (id_area, name) VALUES (6, 'Campos de Coordenação');
INSERT INTO subareas (id_area, name) VALUES (6, 'Não-Metais e seus Compostos');
INSERT INTO subareas (id_area, name) VALUES (6, 'Compostos Organo-Metálicos');
INSERT INTO subareas (id_area, name) VALUES (6, 'Determinação de Estruturas de Compostos Inorgânicos');
INSERT INTO subareas (id_area, name) VALUES (6, 'Foto-Química Inorgânica');
INSERT INTO subareas (id_area, name) VALUES (6, 'Físico Química Inorgânica');
INSERT INTO subareas (id_area, name) VALUES (6, 'Química Bio-Inorgânica');
INSERT INTO subareas (id_area, name) VALUES (6, 'Físico-Química');
INSERT INTO subareas (id_area, name) VALUES (6, 'Cinética Química e Catálise');
INSERT INTO subareas (id_area, name) VALUES (6, 'Eletroquímica');
INSERT INTO subareas (id_area, name) VALUES (6, 'Espectroscopia');
INSERT INTO subareas (id_area, name) VALUES (6, 'Química de Interfaces');
INSERT INTO subareas (id_area, name) VALUES (6, 'Química do Estado Condensado');
INSERT INTO subareas (id_area, name) VALUES (6, 'Química Nuclear e Radioquímica');
INSERT INTO subareas (id_area, name) VALUES (6, 'Química Teórica');
INSERT INTO subareas (id_area, name) VALUES (6, 'Termodinâmica Química');
INSERT INTO subareas (id_area, name) VALUES (6, 'Química Analítica');
INSERT INTO subareas (id_area, name) VALUES (6, 'Separação');
INSERT INTO subareas (id_area, name) VALUES (6, 'Métodos Óticos de Análise');
INSERT INTO subareas (id_area, name) VALUES (6, 'Eletroanálitica');
INSERT INTO subareas (id_area, name) VALUES (6, 'Gravimetria');
INSERT INTO subareas (id_area, name) VALUES (6, 'Titimetria');
INSERT INTO subareas (id_area, name) VALUES (6, 'Instrumentação Analítica');
INSERT INTO subareas (id_area, name) VALUES (6, 'Análise de Traços e Química Ambiental');

-- Inserindo dados na tabela 'areas'
INSERT INTO areas (name) VALUES ('Geociências');

-- Inserindo dados na tabela 'subareas'
INSERT INTO subareas (id_area, name) VALUES (7, 'Geologia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Mineralogia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Petrologia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geoquímica');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geologia Regional');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geotectônica');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geocronologia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Cartografia Geológica');
INSERT INTO subareas (id_area, name) VALUES (7, 'Metalogenia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Hidrogeologia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Prospecção Mineral');
INSERT INTO subareas (id_area, name) VALUES (7, 'Sedimentologia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Paleontologia Estratigráfica');
INSERT INTO subareas (id_area, name) VALUES (7, 'Estratigrafia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geologia Ambiental');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geofísica');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geomagnetismo');
INSERT INTO subareas (id_area, name) VALUES (7, 'Sismologia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geotermia e Fluxo Térmico');
INSERT INTO subareas (id_area, name) VALUES (7, 'Propriedades Físicas das Rochas');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geofísica Nuclear');
INSERT INTO subareas (id_area, name) VALUES (7, 'Sensoriamento Remoto');
INSERT INTO subareas (id_area, name) VALUES (7, 'Aeronomia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Desenvolvimento de Instrumentação Geofísica');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geofísica Aplicada');
INSERT INTO subareas (id_area, name) VALUES (7, 'Gravimetria');
INSERT INTO subareas (id_area, name) VALUES (7, 'Meteorologia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Meteorologia Dinâmica');
INSERT INTO subareas (id_area, name) VALUES (7, 'Meteorologia Sinótica');
INSERT INTO subareas (id_area, name) VALUES (7, 'Meteorologia Física');
INSERT INTO subareas (id_area, name) VALUES (7, 'Química da Atmosfera');
INSERT INTO subareas (id_area, name) VALUES (7, 'Instrumentação Meteorológica');
INSERT INTO subareas (id_area, name) VALUES (7, 'Climatologia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Micrometeorologia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Sensoriamento Remoto da Atmosfera');
INSERT INTO subareas (id_area, name) VALUES (7, 'Meteorologia Aplicada');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geodésia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geodésia Física');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geodésia Geométrica');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geodésia Celeste');
INSERT INTO subareas (id_area, name) VALUES (7, 'Fotogrametria');
INSERT INTO subareas (id_area, name) VALUES (7, 'Cartografia Básica');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geografia Física');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geomorfologia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Climatologia Geográfica');
INSERT INTO subareas (id_area, name) VALUES (7, 'Pedologia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Hidrogeografia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geoecologia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Fotogeografia (Físico-Ecológica)');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geocartografia');
INSERT INTO subareas (id_area, name) VALUES (7, 'Oceanografia Física');
INSERT INTO subareas (id_area, name) VALUES (7, 'Variáveis Físicas da Água do Mar');
INSERT INTO subareas (id_area, name) VALUES (7, 'Movimento da Água do Mar');
INSERT INTO subareas (id_area, name) VALUES (7, 'Origem das Massas de Água');
INSERT INTO subareas (id_area, name) VALUES (7, 'Interação do Oceano com o Leito do Mar');
INSERT INTO subareas (id_area, name) VALUES (7, 'Interação do Oceano com a Atmosfera');
INSERT INTO subareas (id_area, name) VALUES (7, 'Oceanografia Química');
INSERT INTO subareas (id_area, name) VALUES (7, 'Propriedades Químicas da Água do Mar');
INSERT INTO subareas (id_area, name) VALUES (7, 'Inter.Quím.-Biol./Geol. das Subst. Quim. da Água do Mar');
INSERT INTO subareas (id_area, name) VALUES (7, 'Oceanografia Geológica');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geomorfologia Submarina');
INSERT INTO subareas (id_area, name) VALUES (7, 'Sedimentologia Marinha');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geofísica Marinha');
INSERT INTO subareas (id_area, name) VALUES (7, 'Geoquímica Marinha');

INSERT INTO areas (name) VALUES ('Genética');
INSERT INTO subareas (id_area, name) VALUES (8, 'Genética Quantitativa');
INSERT INTO subareas (id_area, name) VALUES (8, 'Genética Molecular e de Microorganismos');
INSERT INTO subareas (id_area, name) VALUES (8, 'Genética Vegetal');
INSERT INTO subareas (id_area, name) VALUES (8, 'Genética Animal');
INSERT INTO subareas (id_area, name) VALUES (8, 'Genética Humana e Médica');
INSERT INTO subareas (id_area, name) VALUES (8, 'Mutagênese');

INSERT INTO areas (name) VALUES ('Morfologia');
INSERT INTO subareas (id_area, name) VALUES (9, 'Citologia e Biologia Celular');
INSERT INTO subareas (id_area, name) VALUES (9, 'Embriologia');
INSERT INTO subareas (id_area, name) VALUES (9, 'Histologia');
INSERT INTO subareas (id_area, name) VALUES (9, 'Anatomia');
INSERT INTO subareas (id_area, name) VALUES (9, 'Anatomia Humana');
INSERT INTO subareas (id_area, name) VALUES (9, 'Anatomia Animal');

INSERT INTO areas (name) VALUES ('Fisiologia');
INSERT INTO subareas (id_area, name) VALUES (10, 'Fisiologia Geral');
INSERT INTO subareas (id_area, name) VALUES (10, 'Fisiologia dos Órgãos e Sistemas');
INSERT INTO subareas (id_area, name) VALUES (10, 'Neurofisiologia');
INSERT INTO subareas (id_area, name) VALUES (10, 'Fisiologia Cardiovascular');
INSERT INTO subareas (id_area, name) VALUES (10, 'Fisiologia da Respiração');
INSERT INTO subareas (id_area, name) VALUES (10, 'Fisiologia Renal');
INSERT INTO subareas (id_area, name) VALUES (10, 'Fisiologia Endócrina');
INSERT INTO subareas (id_area, name) VALUES (10, 'Fisiologia da Digestão');
INSERT INTO subareas (id_area, name) VALUES (10, 'Cinesiologia');
INSERT INTO subareas (id_area, name) VALUES (10, 'Fisiologia do Esforço');
INSERT INTO subareas (id_area, name) VALUES (10, 'Fisiologia Comparada');

INSERT INTO areas (name) VALUES ('Bioquímica');
INSERT INTO subareas (id_area, name) VALUES (11, 'Química de Macromoléculas');
INSERT INTO subareas (id_area, name) VALUES (11, 'Proteínas');
INSERT INTO subareas (id_area, name) VALUES (11, 'Lipídeos');
INSERT INTO subareas (id_area, name) VALUES (11, 'Glicídeos');
INSERT INTO subareas (id_area, name) VALUES (11, 'Bioquímica dos Microorganismos');
INSERT INTO subareas (id_area, name) VALUES (11, 'Metabolismo e Bioenergética');
INSERT INTO subareas (id_area, name) VALUES (11, 'Biologia Molecular');
INSERT INTO subareas (id_area, name) VALUES (11, 'Enzimologia');

INSERT INTO areas (name) VALUES ('Biofísica');
INSERT INTO subareas (id_area, name) VALUES (12, 'Biofísica Molecular');
INSERT INTO subareas (id_area, name) VALUES (12, 'Biofísica Celular');
INSERT INTO subareas (id_area, name) VALUES (12, 'Biofísica de Processos e Sistemas');
INSERT INTO subareas (id_area, name) VALUES (12, 'Radiologia e Fotobiologia');

INSERT INTO areas (name) VALUES ('Farmacologia');
INSERT INTO subareas (id_area, name) VALUES (13, 'Farmacologia Geral');
INSERT INTO subareas (id_area, name) VALUES (13, 'Farmacocinética');
INSERT INTO subareas (id_area, name) VALUES (13, 'Biodisponibilidade');
INSERT INTO subareas (id_area, name) VALUES (13, 'Farmacologia Autonômica');
INSERT INTO subareas (id_area, name) VALUES (13, 'Neuropsicofarmacologia');
INSERT INTO subareas (id_area, name) VALUES (13, 'Farmacologia Cardiorenal');
INSERT INTO subareas (id_area, name) VALUES (13, 'Farmacologia Bioquímica e Molecular');
INSERT INTO subareas (id_area, name) VALUES (13, 'Etnofarmacologia');
INSERT INTO subareas (id_area, name) VALUES (13, 'Toxicologia');
INSERT INTO subareas (id_area, name) VALUES (13, 'Farmacologia Clínica');

INSERT INTO areas (name) VALUES ('Imunologia');
INSERT INTO subareas (id_area, name) VALUES (14, 'Imunoquímica');
INSERT INTO subareas (id_area, name) VALUES (14, 'Imunologia Celular');
INSERT INTO subareas (id_area, name) VALUES (14, 'Imunogenética');
INSERT INTO subareas (id_area, name) VALUES (14, 'Imunologia Aplicada');

INSERT INTO areas (name) VALUES ('Microbiologia');
INSERT INTO subareas (id_area, name) VALUES (15, 'Biologia e Fisiologia dos Microorganismos');
INSERT INTO subareas (id_area, name) VALUES (15, 'Virologia');
INSERT INTO subareas (id_area, name) VALUES (15, 'Bacteriologia');
INSERT INTO subareas (id_area, name) VALUES (15, 'Micologia');
INSERT INTO subareas (id_area, name) VALUES (15, 'Microbiologia Aplicada');
INSERT INTO subareas (id_area, name) VALUES (15, 'Microbiologia Médica');
INSERT INTO subareas (id_area, name) VALUES (15, 'Microbiologia Industrial e de Fermentação');

INSERT INTO areas (name) VALUES ('Parasitologia');
INSERT INTO subareas (id_area, name) VALUES (16, 'Protozoologia de Parasitos');
INSERT INTO subareas (id_area, name) VALUES (16, 'Protozoologia Parasitária Humana');
INSERT INTO subareas (id_area, name) VALUES (16, 'Protozoologia Parasitária Animal');
INSERT INTO subareas (id_area, name) VALUES (16, 'Helmintologia de Parasitos');
INSERT INTO subareas (id_area, name) VALUES (16, 'Helmintologia Humana');
INSERT INTO subareas (id_area, name) VALUES (16, 'Helmintologia Animal');
INSERT INTO subareas (id_area, name) VALUES (16, 'Entomologia e Malacologia de Parasitos e Vetores');

INSERT INTO areas (name) VALUES ('Ecologia');
INSERT INTO subareas (id_area, name) VALUES (17, 'Ecologia Teórica');
INSERT INTO subareas (id_area, name) VALUES (17, 'Ecologia de Ecossistemas');
INSERT INTO subareas (id_area, name) VALUES (17, 'Ecologia Aplicada');

INSERT INTO areas (name) VALUES ('Oceanografia');
INSERT INTO subareas (id_area, name) VALUES (18, 'Oceanografia Biológica');
INSERT INTO subareas (id_area, name) VALUES (18, 'Inter. entre os Organ. Marinhos e os Parâmetros Ambientais');

INSERT INTO areas (name) VALUES ('Botânica');
INSERT INTO subareas (id_area, name) VALUES (19, 'Paleobotânica');
INSERT INTO subareas (id_area, name) VALUES (19, 'Morfologia Vegetal');
INSERT INTO subareas (id_area, name) VALUES (19, 'Morfologia Externa');
INSERT INTO subareas (id_area, name) VALUES (19, 'Citologia Vegetal');
INSERT INTO subareas (id_area, name) VALUES (19, 'Anatomia Vegetal');
INSERT INTO subareas (id_area, name) VALUES (19, 'Palinologia');
INSERT INTO subareas (id_area, name) VALUES (19, 'Fisiologia Vegetal');
INSERT INTO subareas (id_area, name) VALUES (19, 'Nutrição e Crescimento Vegetal');
INSERT INTO subareas (id_area, name) VALUES (19, 'Reprodução Vegetal');
INSERT INTO subareas (id_area, name) VALUES (19, 'Ecofisiologia Vegetal');
INSERT INTO subareas (id_area, name) VALUES (19, 'Taxonomia Vegetal');
INSERT INTO subareas (id_area, name) VALUES (19, 'Taxonomia de Criptógamos');
INSERT INTO subareas (id_area, name) VALUES (19, 'Taxonomia de Fanerógamos');
INSERT INTO subareas (id_area, name) VALUES (19, 'Fitogeografia');
INSERT INTO subareas (id_area, name) VALUES (19, 'Botânica Aplicada');

INSERT INTO areas (name) VALUES ('Zoologia');
INSERT INTO subareas (id_area, name) VALUES (20, 'Paleozoologia');
INSERT INTO subareas (id_area, name) VALUES (20, 'Morfologia dos Grupos Recentes');
INSERT INTO subareas (id_area, name) VALUES (20, 'Fisiologia dos Grupos Recentes');
INSERT INTO subareas (id_area, name) VALUES (20, 'Comportamento Animal');
INSERT INTO subareas (id_area, name) VALUES (20, 'Taxonomia dos Grupos Recentes');
INSERT INTO subareas (id_area, name) VALUES (20, 'Zoologia Aplicada');
INSERT INTO subareas (id_area, name) VALUES (20, 'Conservação das Espécies Animais');
INSERT INTO subareas (id_area, name) VALUES (20, 'Utilização dos Animais');
INSERT INTO subareas (id_area, name) VALUES (20, 'Controle Populacional de Animais');