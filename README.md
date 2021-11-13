# HackPleasanterApi

## これは何?

Pleasanter のサイト設定から、
api 通信定義や、sql 用の view を生成する補助プログラム郡です。
このプログラムは以下 2 種類のプログラムにより構成されています。

| No | プログラム名  | 概要  |
| --- | --- | --- |
| 1 | HackPleasanterApi.Generator.JsonDefinitionExtractor  |  サイトパッケージ情報から生成項目の編集に必要なcsvを作成します。 |
| 2 | HackPleasanterApi.Generator.CodeGenerator  | csvデータから実際に稼働するソースコードを生成します。

このプログラムの使い方は3段階で使用します。

- 一段回目では、JsonDefinitionExtractorを用いて、プリザンターのサイト設定からコード生成に必要な情報を抽出して編集可能なCSV形式で展開します。

- 抽出されたCSVデータの中から、ユーザーがコード生成に必要な部分だけ選択してcsvを編集します。

- HackPleasanterApi.Generator.CodeGeneratorを用いて、csvから実際のコード生成を行います。

### HackPleasanterApi.Generator.JsonDefinitionExtractor

サイトパッケージ情報(JSON)から必要な情報を抽出します。
入出力ファイル名は固定となっているので、
指定された内容をプログラム実行パスに配備して下さい。


| No | 入力/出力 |名称 | 概要  |
| --- | --- | --- | --- |
|1|入力|export.json|プリザンターから出力されたサイトパッケージ|
|2|出力|Interface.csv|個別の項目情報|
|2|出力|Sites.csv|サイト情報|

### CSV ファイル形式

編集対象のCSVファイルは以下2種類となります。
それぞれの編集内容は以下となります。

必要なカラムに関して「IsTarget」をtrueに指定して下さい。

#### Sites.csv

csvファイルサンプル

```
SiteId,Title,IsTarget,SiteVariableName,Memo
2,API試験用,False,,
3,記録テーブル,true,RecordingTable,
```

重要列

| No | 列名 | 概要  |
| --- | --- | --- |
|1|SiteId|サイトID情報|
|2|Title|表示名|
|3|IsTarget|★コード生成ターゲットとするか?|
|4|SiteVariableName|★コードとして指定される変数名|
|5|Memo|メモ|


#### Interface.csv

csvファイルサンプル

```
Title,SiteId,ParentId,InheritPermission,Description,ValidateRequired,ColumnName,LabelText,VariableName,IsTarget,ChoicesText
記録テーブル,3,0,0,CheckA,,CheckA,,CheckA,true,
記録テーブル,3,0,0,TypeA,,ClassA,,TypeA,true,
記録テーブル,3,0,0,DataA,,DateA,,DataA,true,
記録テーブル,3,0,0,StringA,,DescriptionA,,StringA,true,
記録テーブル,3,0,0,NumA,,NumA,,NumA,true,
```

重要列

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


### HackPleasanterApi.Generator.CodeGenerator

設定したcsvデータを用いて実際のコードを生成します。


## docker

docker run -v /Users/ayusawayuusuke/Documents/dev/github/HackPleasanterApi:/var/data -it test bash

docker build -t hack_pleasanter_api:latest .

docker run --rm -v $(pwd):/var/data -it hack_pleasanter_api:latest bash

docker run --rm -v $(pwd)/Working:/var/data -it hack_pleasanter_api:latest bash

/app/publish/HackPleasanterApi.Generator.CodeGenerator

CodeGeneratorConfig.xml

docker run --rm -v $(pwd):/var/data -it hack_pleasanter_api:latest /app/publish/HackPleasanterApi.Generator.JsonDefinitionExtractor Export /var/data/Working/DefinitionExtractorConfig.xml

docker run --rm -w /var/data/Working -v $(pwd):/var/data hack_pleasanter_api:latest /app/publish/HackPleasanterApi.Generator.JsonDefinitionExtractor Export /var/data/Working/DefinitionExtractorConfig.xml
