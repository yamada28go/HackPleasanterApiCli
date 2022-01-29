# HackPleasanterApi

## これは何?

(Pleasanter)[https://pleasanter.org/] のサイトパッケージから、
api 通信定義や、sql 用の view を生成する補助プログラム郡です。
このプログラムは以下 2 種類の大きな機能より構成されています。

| No | プログラム名  | 概要  |
| --- | --- | --- |
| 1 | JsonDefinitionExtractor  |  サイトパッケージ情報から生成項目の編集に必要なcsvを作成します。 |
| 2 | CodeGenerator  | csvデータから実際に稼働するソースコードを生成します。

このプログラムの使い方は以下の3段階で使用します。

- 段階1 : JsonDefinitionExtractorを用いて、プリザンターのサイトパッケージからコード生成に必要な情報を抽出して編集可能なCSV形式で展開します。

- 段階2 : 抽出されたCSVデータの中から、ユーザーがコード生成に必要な部分の選択をcsvファイルにし行います。

- 段階3 : HackPleasanterApi.Generator.CodeGeneratorを用いて、csvから実際のコード生成を行います。

---
## 配布形式

本プログラムは.Net Core環境で実装されています。
.Net Core環境は可搬可能な形式ではありますが、ホスト側に対象となる環境を用意する必要があります。
このため、環境の持ち運びが便利なdocer環境での配布を基本形式としています。

コマンドのヘルプ出力例を以下に示します。
該当コマンドはコンテナ内で稼働するため、コマンドの動作に使用する作業ディレクトリ(ホストとマウントする事を想定)を第一引数として引き渡す必要があります。作業ディレクトリを引き渡した後、サブコマンドとして実際に作業を指定するコマンドを追加します。

```

EntranceCommand
  Pleasanter インターフェースコード生成

Usage:
  EntranceCommand [options] <WorkingDirectory> [command]

Arguments:
  <WorkingDirectory>  コマンドの作業ディレクトリ

Options:
  --version       Show version information
  -?, -h, --help  Show help and usage information

Commands:
  JsonDefinitionExtractor  プリザンターとのサイト構成からインターフェース定義を抽出する。
  CodeGenerator            プリザンターとのインターフェースコードを生成する。

```

コマンドの実行例として以下を以下に示します。

```
docker run --rm -v $(pwd)/Working:/local example /local CodeGenerator Generate CodeGeneratorConfig.xml

```

## テンプレート

本コマンドは(Pleasanter)[https://pleasanter.org/]用のグルーコードを生成するアプリとなりますが、
生成対象のテンプレートを切り替えることでいろいろな種別の処理に対応することができます。

本コマンドを使用するためには、
生成対象のテンプレートデータを準備する必要があります。
テンプレートデータは以下サイトで取得する事ができます。

| No | 名称 | 概要 | URL|
| --- | --- | --- |--- | 
|1| HackPleasanterApi.Csharp | C#用ライブラリ | https://github.com/yamada28go/HackPleasanterApi.Csharp|
|2| HackPleasanterApi.PostgreSQL | PostgreSQL用 View生成ライブラリ | https://github.com/yamada28go/HackPleasanterApi.PostgreSQL|

---
## JsonDefinitionExtractor

サイトパッケージ情報(JSON)から必要な情報を抽出します。

### 設定ファイル作成

コマンドを動作させるためには、動作設定ファイルが必要となります。
設定ファイル雛形はコマンドから生成させることができます。

```
docker --run -v $(pwd)/Working:/local yamada28go/hack-pleasanter-api-cli /local JsonDefinitionExtractor Generate CodeGeneratorConfig.xml
```

生成される設定ファイルの例を以下に示します。
サイト情報の抽出では、これらの設定ファイルを用いてコマンドの動作を設定します。

```
<?xml version="1.0" encoding="utf-8"?>
<DefinitionExtractorConfig xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Input>
    <!-- 読み込み対象となるサイトパッケージファイル -->
    <SiteExportDefinitionFile>export.json</SiteExportDefinitionFile>
  </Input>
  <Output>
    <!-- サイト情報の出力ファイル -->
    <SiteDefinitionFile>Sites.csv</SiteDefinitionFile>
    <!-- インターフェース情報の出力ファイル -->
    <InterfaceDefinitionFile>Interface.csv</InterfaceDefinitionFile>
    <!-- 説明文字列を生成コードの変数名として使用するか -->    
    <UseDescriptionAsVariableName>true</UseDescriptionAsVariableName>
  </Output>
</DefinitionExtractorConfig>
```

設定ファイルの各項目の意味は以下となります。
各ファイルのパスは相対参照となり、コマンドの第一引数で指定した値をベースパスとして動作します。

| No | 入力/出力 | 項目名 |設定値 | 概要  |
| --- | --- | --- | --- | --- |
|1|入力|SiteExportDefinitionFile|export.json|プリザンターから出力されたサイトパッケージ|
|2|出力|SiteDefinitionFile|Interface.csv|個別の項目情報|
|2|出力|InterfaceDefinitionFile|Sites.csv|サイト情報|

### CSV展開

サイトパッケージファイルから、CSVファイルを抽出します。
抽出に使用するコマンド例は以下となります。

実行に成功すると設定ファイルで指定した2個のcsvファイルが生成されます。

```
docker run -v $(pwd)/Working:/local yamada28go/hack-pleasanter-api-cli /local JsonDefinitionExtractor Generate CodeGeneratorConfig.xml
```


### CSV ファイル形式

編集対象のCSVファイルは以下2種類となります。
それぞれの編集内容は以下となります。

必要なカラムに関して「IsTarget」をtrueに指定して下さい。

##### Sites.csv

コード生成対象のサイトを管理するためのCSVとなります。

csvファイルサンプル

```
SiteId,Title,IsTarget,SiteVariableName,Memo
2,API試験用,False,,
3,記録テーブル,true,RecordingTable,
```

各列の意味は以下となります。
主として、No3,No4を調整することとなのります。

| No | 列名 | 概要  |
| --- | --- | --- |
|1|SiteId|サイトID情報|
|2|Title|プリザンター上での表示名|
|3|IsTarget|★コード生成ターゲットとするか?|
|4|SiteVariableName|★コードとして生成されるテーブル名|
|5|Memo|メモ|


##### Interface.csv

コード生成対象となる、サイト内のカラムを管理するためのCSV定義となります。

csvファイルサンプル

```
Title,SiteId,ParentId,InheritPermission,Description,ValidateRequired,ColumnName,LabelText,VariableName,IsTarget,ChoicesText
記録テーブル,3,0,0,CheckA,,CheckA,,CheckA,true,
記録テーブル,3,0,0,TypeA,,ClassA,,TypeA,true,
記録テーブル,3,0,0,DataA,,DateA,,DataA,true,
記録テーブル,3,0,0,StringA,,DescriptionA,,StringA,true,
記録テーブル,3,0,0,NumA,,NumA,,NumA,true,
```

これらのうち、No9、No10を指定してコード生成を行います。


| No | 列名 | 概要  |
| --- | --- | --- |
|1|Title|タイトル|
|2|SiteId|SiteId|
|3|ParentId|ParentId|
|4|InheritPermission|InheritPermission|
|5|Description|Description|
|6|ValidateRequired|ValidateRequired|
|7|ColumnName|ColumnName|
|8|LabelText|LabelText|
|9|VariableName|★コード生成時に使用される変数名|
|10|IsTarget|★コード生成対象として指定するか|
|11|ChoicesText|ChoicesText|


---
## CodeGenerator

設定したcsvデータを用いて実際のコードを生成します。
サイトパッケージの展開と同じく、コマンドの動作詳細は設定フアイルを用いて行います。
設定ファイルの

```
docker --run -v $(pwd)/Working:/local example /local CodeGenerator
```

```
CodeGenerator
  プリザンターとのインターフェースコードを生成する。

Usage:
  EntranceCommand [options] <WorkingDirectory> CodeGenerator [command]

Arguments:
  <WorkingDirectory>  コマンドの作業ディレクトリ

Options:
  -?, -h, --help  Show help and usage information

Commands:
  Generate <ConfigurationFileName>    コード生成の設定に使用するデフォルトの設定ファイルを取得します。
  DefaultConfiguration <OutFileName>  コード生成の設定に使用するデフォルトの設定ファイルを取得します。
```

### 設定ファイル作成

コマンドを動作させるためには、動作設定ファイルが必要となります。
設定ファイル雛形はコマンドから生成させることができます。

```
docker --run -v $(pwd)/Working:/local yamada28go/hack-pleasanter-api-cli /local JsonDefinitionExtractor Generate CodeGeneratorConfig.xml
```
生成される設定ファイルの例を以下に示します。
コード生成処理では、これらの設定ファイルを用いてコマンドの動作を設定します。

なお、設定ファイルの中で指定される各種設定フアイルはコマンドの第一引数で指定されたパスを起点のディレクトリとして動作します。

コード生成時の設定ファイルは生成対象のテンプレートに付属します。
ユーザーが実際に調整が必要な項目はNameSpace程度となります。

```
<?xml version="1.0" encoding="utf-8"?>
<GeneratorConfig xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <InputFiles>
    <!-- 読み込み対象のサイト構成ファイル名 -->
    <SiteDefinitionFile>Sites.csv</SiteDefinitionFile>
    <!-- 読み込み対象のインターフェースファイル名 -->
    <InterfaceDefinitionFile>Interface.csv</InterfaceDefinitionFile>
    <!-- CSVファイルのエンコーディング -->
    <Encoding>Shift_JIS</Encoding>
  </InputFiles>
  <OutputConfig>
    <!-- 生成結果出力ディレクトリ -->
    <OutputDirectory>Generated</OutputDirectory>
  </OutputConfig>
  <TemplateFiles>
    <!-- 読み込み対象のテンプレートファイル名 -->
    <TemplateFiles>
      <TemplateFileName>Templates/CSharp/ServiceTemplate.txt</TemplateFileName>
      <OutputSubdirectoryName>Services</OutputSubdirectoryName>
      <HeadPrefix />
      <EndPrefix>Service</EndPrefix>
      <OutputExtension>cs</OutputExtension>
      <Encoding>Shift_JIS</Encoding>
    </TemplateFiles>
    <TemplateFiles>
      <TemplateFileName>Templates/CSharp/ModelTemplate.txt</TemplateFileName>
      <OutputSubdirectoryName>Models</OutputSubdirectoryName>
      <HeadPrefix />
      <EndPrefix>Model</EndPrefix>
      <OutputExtension>cs</OutputExtension>
      <Encoding>Shift_JIS</Encoding>
    </TemplateFiles>
  </TemplateFiles>
  <CodeConfig>
    <!-- コード生成時に使用される名前空間 -->
    <NameSpace />
  </CodeConfig>
</GeneratorConfig>
```


### コード生成

CSVファイルからコードを生成します。
抽出に使用するコマンド例は以下となります。
コマンドが実行されるとXMLで指定されたディレクトリにコードが生成されます。

```
docker run -v $(pwd)/Working:/local yamada28go/hack-pleasanter-api-cli /local CodeGenerator Generate CodeGeneratorConfig.xml
```

