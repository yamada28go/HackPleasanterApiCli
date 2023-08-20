# HackPleasanterApi

## これは何?

[Pleasanter](https://pleasanter.org/) のサイトパッケージから、
api 通信定義や、sql 用の view を生成する補助プログラム郡です。

詳細に処理を調整したい人は「README_Detail.md」を参照してください。
本資料では簡単に使えるバージョンの使い方を説明します。

---
## 配布形式

本プログラムは.Net Core環境で実装されています。
実行にはホスト側に環境を定義する必用があります。
環境の持ち運びが便利なdocer環境での配布を基本形式としています。

### docker環境でのビルド方法

本ファイルが存在するディレクトリで以下コマンドを実行してください。

```
docker build . -t hack-pleasanter-api-cli:latest
```

### dockerでの起動方法

ビルドに成功したら、以下コマンドで処理を起動します。

```
docker run --rm -v $(pwd)/Working:/local hack-pleasanter-api-cli:latest /local SimpleCommand export.json
```

これらのコマンドは以下意味を持ちます。
重要なポイントは、**No3 ,No7**となります。
ここのパラメータで取り込みを行うサイト情報ファイルを指定します。 

| No | コマンド引数 | 説明 |
| -- | --- | --- |
|1| `docker run` | Dockerコンテナを実行するコマンド |
|2| `--rm` | コンテナが停止した場合、自動的に削除するオプション |
|3| `-v $(pwd)/Working:/local` | ローカルディレクトリ `$(pwd)/Working` を、コンテナ内の `/local` という名前のディレクトリにマウントします。 |
|4| `hack-pleasanter-api-cli:latest` | Dockerイメージ `hack-pleasanter-api-cli` の最新バージョンを指定するオプション |
|5| `/local` | `/local` ディレクトリを作業用のディレクトリと指定します。サイト情報の読み込み、生成されたコードの出力はここのパスより実行されます。 |
|6| `SimpleCommand` | `SimpleCommand` コマンドを実行します。サイト情報を読み込み、グルーコードを生成します。 |
|7| `export.json` | 指定されたファイルをエクスポートされたサイト情報として読み取ります。 |

### docker hubの場合

docker hubでも配布しています。
実行コマンドは以下となります。

bashの場合

```
docker run --rm -v $(pwd)/Working:/local yamada28go/hack-pleasanter-api-cli2:0.6 /local SimpleCommand export.json
```

windowsの場合

```
docker run --rm -v %CD%/Working:/local yamada28go/hack-pleasanter-api-cli2:0.6 /local SimpleCommand export.json
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
実行ライブラリが必用となります。
ライブラリはの情報は以下を参照してください。

[C#用](https://github.com/yamada28go/HackPleasanterApi.Csharp)

[TypeScrypt](https://github.com/yamada28go/HackPleasanterApi.ScriptTs)

[PostgreSQL](https://github.com/yamada28go/HackPleasanterApi.PostgreSQL)

