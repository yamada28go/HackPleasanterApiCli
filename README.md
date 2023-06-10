# HackPleasanterApi

## これは何?

(Pleasanter)[https://pleasanter.org/] のサイトパッケージから、
api 通信定義や、sql 用の view を生成する補助プログラム郡です。

詳細に処理を調整したい人は「README_Detail.md」を参照してください。
本資料では簡単に使えるバージョンの使い方を説明します。

---
## 配布形式

本プログラムは.Net Core環境で実装されています。
.Net Core環境は可搬可能な形式ではありますが、ホスト側に対象となる環境を用意する必要があります。
このため、環境の持ち運びが便利なdocer環境での配布を基本形式としています。

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

| コマンド引数 | 説明 |
| --- | --- |
| `docker run` | Dockerコンテナを実行するコマンド |
| `--rm` | コンテナが停止した場合、自動的に削除するオプション |
| `-v $(pwd)/Working:/local` | ローカルディレクトリ `$(pwd)/Working` を、コンテナ内の `/local` という名前のディレクトリにマウントするオプション |
| `hack-pleasanter-api-cli:latest` | Dockerイメージ `hack-pleasanter-api-cli` の最新バージョンを指定するオプション |
| `/local` | `/local` ディレクトリを作業用のディレクトリと指定します。サイト情報の読み込み、生成されたコードの出力はここのパスより実行されます。 |
| `SimpleCommand` | `SimpleCommand` コマンドを実行します。サイト情報を読み込み、グルーコードを生成します。 |
| `export.json` | 指定されたファイルをエクスポートされたサイト情報として読み取ります。 |


