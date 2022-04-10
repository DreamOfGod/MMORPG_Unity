//===============================================
//作    者：
//创建时间：2022-04-10 15:11:24
//备    注：
//===============================================

/// <summary>
/// 统计
/// </summary>
public class Statistics
{
    public static void Init()
    {
        //appid
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="nickname"></param>
    public static void Register(int userId, string nickname)
    {

    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="nickname"></param>
    public static void Logon(int userId, string nickname)
    {

    }

    /// <summary>
    /// 修改昵称
    /// </summary>
    /// <param name="nickname"></param>
    public static void ChangeNickname(string nickname)
    {

    }

    /// <summary>
    /// 升级
    /// </summary>
    /// <param name="level"></param>
    public static void UpLevel(int level)
    {

    }

    //======================================================任务
    /// <summary>
    /// 任务开始
    /// </summary>
    public static void TaskBegin(int taskId, string taskName)
    {

    }

    /// <summary>
    /// 任务结束
    /// </summary>
    /// <param name="taskId"></param>
    /// <param name="taskName"></param>
    /// <param name="status"></param>
    public static void TaskEnd(int taskId, string taskName, int status)
    {

    }

    //======================================================关卡
    /// <summary>
    /// 关卡开始
    /// </summary>
    public static void GameLevelBegin(int gameLevelId, string gameLevelName)
    {

    }

    /// <summary>
    /// 关卡结束
    /// </summary>
    public static void GameLevelEnd(int gameLevelId, string gameLevelName, int status, int star)
    {

    }

    /// <summary>
    /// 开始充值
    /// </summary>
    /// <param name="orderId">订单号</param>
    /// <param name="productId">产品编号</param>
    /// <param name="money">充值金额</param>
    /// <param name="type">币种</param>
    /// <param name="virtualMoney">虚拟货币</param>
    /// <param name="channelID">渠道号</param>
    public static void ChargeBegin(string orderId, string productId, double money, string type, double virtualMoney, string channelID)
    {

    }

    /// <summary>
    /// 充值完成
    /// </summary>
    public static void ChargeEnd()
    {

    }

    /// <summary>
    /// 购买道具
    /// </summary>
    /// <param name="itemId">道具编号</param>
    /// <param name="itemName">名称</param>
    /// <param name="price">价格</param>
    /// <param name="count">数量</param>
    public static void BuyItem(int itemId, string itemName, int price, int count)
    {

    }

    /// <summary>
    /// 消耗道具
    /// </summary>
    /// <param name="itemId">道具编号</param>
    /// <param name="itemName">名称</param>
    /// <param name="count">数量</param>
    /// <param name="useType">用途</param>
    public static void ItemUsed(int itemId, string itemName, int count, int useType)
    {

    }

    /// <summary>
    /// 自定义事件
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void AddEvent(string key, string value)
    {

    }
}
