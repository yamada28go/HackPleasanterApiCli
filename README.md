# HackPleasanterApi

## これは何?

[Pleasanter](https://pleasanter.org/) の[エクスポートされたサイトパッケージ](https://pleasanter.org/manual/site-package-export)から、
api 通信定義や、sql 用の view を生成する補助プログラム郡です。

## 配布形式

本プログラムは.Net Core環境で実装されています。
環境の持ち運びが便利なDocker環境での配布を基本形式としています。

### docker環境でのビルド方法

本ファイルが存在するディレクトリで以下コマンドを実行してください。

```
docker build . -t hack-pleasanter-api-cli:latest
```

### dockerでの起動方法

ビルドに成功したら、以下コマンドで処理を起動します。
(dosの場合、$(pwd)は%CD%に置換。)

```
docker run --rm -v $(pwd):/local hack-pleasanter-api-cli /local Generation export.json
```

これらのコマンドは以下意味を持ちます。
重要なポイントは、**No3とNo7**となります。
ここのパラメータで取り込みを行うサイト情報ファイルを指定します。 

| No | コマンド引数 | 説明 |
| -- | --- | --- |
|1| `docker run` | Dockerコンテナを実行するコマンド |
|2| `--rm` | コンテナが停止した場合、自動的に削除するオプション |
|3| `-v $(pwd):/local` | カレントディレクトリ `$(pwd)` を、コンテナ内の `/local` という名前のディレクトリにマウントします。 |
|4| `hack-pleasanter-api-cli:latest` | Dockerイメージ `hack-pleasanter-api-cli` の最新バージョンを指定します。 |
|5| `/local` | `/local` ディレクトリを作業用のディレクトリと指定します。サイト情報の読み込み、生成されたコードの出力はここのパスより実行されます。 |
|6| `Generation` | `Generation` コマンドを実行します。サイト情報を読み込み、グルーコードを生成します。 |
|7| `export.json` | 指定されたファイルをエクスポートされたサイト情報として読み取ります。作業ディレクトリを基点として動作します。 |

### docker hubの場合

docker hubでも配布しています。
配布場所は以下となります。
[hack-pleasanter-api-cli2](https://hub.docker.com/repository/docker/yamada28go/hack-pleasanter-api-cli2/general)

実行コマンドは以下となります。
( コマンドの違いとしては、$(pwd)か%CD%の差分となります。 )

bashの場合

```bash
docker run --rm -v $(pwd):/local yamada28go/hack-pleasanter-api-cli2:0.10 /local Generation export.json
```

windowsの場合

```dos
docker run --rm -v %CD%:/local yamada28go/hack-pleasanter-api-cli2:0.10 /local Generation export.json
```

## 実行結果

コマンドを実行することで、以下のようなフォルダが出力されます。
これらのディレクトリにはそれぞれの言語用のグルーファイルが生成されています。

```
├─HackPleasanterApi.Csharp.git
│  └─Generated
│      ├─Models
│      └─Services
├─HackPleasanterApi.PostgreSQL
│  └─Generated
│      ├─CreateView
│      └─DropView
├─HackPleasanterApi.ScriptTs
│  └─Generated
│      ├─Models
│      └─Services
```

これらのグルーコードを実行するためには、
対応するライブラリが必要です。
ライブラリはの情報は以下を参照してください。

[C#用](https://github.com/yamada28go/HackPleasanterApi.Csharp)

[TypeScript](https://github.com/yamada28go/HackPleasanterApi.ScriptTs)

[PostgreSQL](https://github.com/yamada28go/HackPleasanterApi.PostgreSQL)


## 設定ファイルによる調整

### 概要

設定ファイルにより生成プログラムの挙動を調整をする事が出来ます。
コマンドのバージョンアップによる生成結果の変化を望まない場合、
設定ファイルを用いて動作を固定して使用する事をお勧めします。

### ひな形の作成

設定ファイルは以下コマンドを実行することで、カレントディレクトリにdata.xlmといファイルが生成されます。


```bash
docker run --rm -v $(pwd):/local yamada28go/hack-pleasanter-api-cli2:0.10 /local  DefaultConfigurationFileGeneration data.xml
```

### 生成の説明

生成される設定ファイルは以下となります。

```xml
<?xml version="1.0" encoding="utf-8"?>
<GenerationSettings xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <CsharpSettings>
    <TemplateVersion>0.3</TemplateVersion>
    <Namespace>PleasanterApiLib</Namespace>
    <ProjectName>PleasanterApiLib</ProjectName>
    <ForcedOverwrite>true</ForcedOverwrite>
  </CsharpSettings>
  <ScriptTsSettings>
    <TemplateVersion>0.4</TemplateVersion>
  </ScriptTsSettings>
  <PostgreSQLSettings>
    <TemplateVersion>0.2</TemplateVersion>
  </PostgreSQLSettings>
</GenerationSettings>
```

各項目の設定パラメータの意味は以下となります。

| 設定セクション          | 項目              | 値               | 説明                                                                       |
|---------------------|-----------------|-----------------|--------------------------------------------------------------------------|
| **CsharpSettings**     | TemplateVersion | 0.3             | C#プロジェクトのテンプレートバージョンを指定します。                              |
|                     | Namespace       | PleasanterApiLib | 生成されるC#コードのための名前空間を指定します。 |
|                     | ProjectName     | PleasanterApiLib | 生成されるプロジェクトの名前を指定します。                                                    |
|                     | ForcedOverwrite | true            | 生成済みのコードがある場合、ファイルの上書きを強制するかどうかを指定します。trueであれば上書きを許可。           |
| **ScriptTsSettings**  | TemplateVersion | 0.4             | TypeScriptスクリプトのテンプレートバージョンを指定します。                         |
| **PostgreSQLSettings** | TemplateVersion | 0.2             | PostgreSQL用のテンプレートバージョンを指定します。                               |

### 設定ファイルを指定したコード生成

`--config ファイル名`で生成するコマンドを指定する事が出来ます。
設定ファイルは指定された作業ディレクトリを基点として参照されます。
(この場合、 localが基点となる。)

```bash
docker run --rm -v $(pwd):/local yamada28go/hack-pleasanter-api-cli2:0.10 /local Generation --config data.xml export.json
```
