# Sophia

graceフロントエンドのバックエンドAPI。

## アーキテクチャ

Clean Architecture / レイヤードアーキテクチャ。

```
Sophia.Api           → Presentation層（Controllers, Responder, Converters）
Sophia.Domain        → Domain層（ドメインロジック）
Sophia.Infrastructure → Infrastructure層（DbContext, Models, Repository）
Sophia.Db            → Migration専用（Migrations, DbContextFactory）
```

**依存関係:** Api → Infrastructure → Domain, Db → Infrastructure

## 規約

- **Controller**: Single-action pattern（1コントローラ = 1アクション、`Invoke()` メソッド）
- **レスポンス**: `BaseResponder<T>` パターンでAPIレスポンスを統一
- **DateTime**: `JstDateTimeConverter` でJST変換
- **DB**: EF Core + Npgsql PostgreSQL, snake_case naming convention
- **Migration**: `Sophia.Db` プロジェクトから実行（`MigrationsAssembly("Sophia.Db")`）

## コマンド

```bash
dotnet build Sophia.sln          # ビルド
task migrate                     # マイグレーション実行（Docker内）
task up                          # Docker起動
task bash                        # Dockerコンテナ内bashを開く
```

## 関連プロジェクト

- **grace**: フロントエンド（Next.js）
