public interface IEnemy
{
    void OnLeftClick();
    void OnRightClickDown();

    void OnHoverEnter();
    void OnHoverExit();

    //To identify different enemy types
    int ID { get; set; }
}
