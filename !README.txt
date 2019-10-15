UpASAP ウェブ開発用SFTPアップロード支援ツール

■概要
・Windows PCの指定ディレクトリ以下を監視し、書き換えられたファイルをリモートサーバにアップロードする。
アップロードプロトコルは SFTP (SSH.NET; Renci.SshNetを採用)
サーバアドレスの選択は GVS GetNetworkMAC コマンド(ブロードキャスト)に対応していて、MAC アドレスで指定することも可能。
ARPテーブルによるMACアドレス - IP変換

・アップロードしたウェブ関連ファイルを複数ブラウザで表示

・端末アプリケーションを起動してリモートホストにSSH接続

設定をXMLファイルに保存。拡張子 .webi (web immediater ini)
最後に使った設定ファイル名をレジストリに記録し、次回起動時特、特に指定されなければ同じファイルを開く。

サーバが頻繁にシャットダウンや再起動されることを考慮

■基本操作
 1. アップロード元ディレクトリを選択

 2. アップロード先、サーバのホスト名、またはIPアドレスまたは MACアドレスを選択

 3. アップロード先、ディレクトリを選択

 4. アップロード先、SFTP用ユーザ＆パスワード設定 (SSH用と共通) 
現仕様ではセキュリティのためパスワードは設定ファイルに保存していない

 5. auto にチェックを入れる

あるいは upload ボタンで一括手動アップロード

link textにURLをドラッグアンドドロップ

■TODO
D&Dによるアップロード
マニュアルコピー　always/timestamp compare
ホスト不在時のアップロード保留
アップロードのスレッド化

拡張子登録 - アクセスが拒否...?
自動/手動の排他？

■
rapi2 B8:27:EB:47:8C:A5
monitor? C4:F3:12:A0:91:1D

regedit ブラウザ設定、ターミナル設定
HKEY_CURRENT_USER\Software\UpASAP

代表的ブラウザパス
Google Chrome
C:\Program Files (x86)\Google\Chrome\Application\chrome.exe
IE11
C:\Program Files\Internet Explorer\iexplore.exe
Mozilla Firefox
C:\Program Files (x86)\Mozilla Firefox\firefox.exe
Vivaldi
C:\Users\t-serikawa\AppData\Local\Vivaldi\Application\vivaldi.exe



HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\CIT\win32k\1705
HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers
HKEY_USERS\S-1-5-21-661334639-1455992492-1260912821-2068\Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Compatibility Assistant\Persisted
HKEY_USERS\S-1-5-21-661334639-1455992492-1260912821-2068\Software\NVIDIA Corporation\Global\nViewProcessManager\11028
