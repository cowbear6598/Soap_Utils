# 使用方法 (Get Started)

## UpdateManager

- 使用 UpdateManager.Instance.RequestUpdate / Remove 來將要每偵要更新的 Function 加入
- 目前只包含常用的 Update & FixedUpdate

## SoapUtils
### 特效

- SoapUtils.EnableEmission 啟用 / 關閉特效
- SoapUtils.GetEmissionRate 取得特效產生數量
- SoapUtils.SetEmissionRate 設定特效產生數量

### 雜湊

- SoapUtils.EncodeToSha256 將字串雜湊成 Sha256
- SoapUtils.EncodeToHMAC_SHA1 將字串雜湊成 HMACSHA1

### UI

- SoapUtils.SetCanvasGroup 快速設定 Canvas Group 的數值
- SoapUtils.SetColorAlpha 快速設定 Alpha

## DesignPattern

- SingeletonMonoBehaviour 單例模式，繼承此程式碼並實作 IsNeedDontDestoryOnLoad 決定是否使用

## UI 相關

- CanvasResolutionHandle 掛在有 Canvas Scaler 的地方，自適應各種比例去調整 Match Width Or Height