public interface IEnemy
{
    void OnLeftClick();
    void OnRightClickDown();

    void OnHoverEnter();
    void OnHoverExit();

    int ID { get; set; }

}
