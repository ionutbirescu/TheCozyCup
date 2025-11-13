namespace TheCozyCup
{
    // A general-purpose delegate for state changes (e.g., MenuItem price changed, OrderLineItem quantity changed).
    public delegate void StateChangedEventHandler(object sender, EventArgs e);
    // Specific delegate for Order changes, explicitly carrying the important new total amount.
    /// This simplifies the UI logic, as it doesn't have to calculate the total itself.
    public delegate void OrderUpdatedEventHandler(object sender, decimal newTotal);
    /// Delegate used by an OrderLineItem to tell its parent Order to remove it (e.g., quantity hit zero).
    public delegate void OrderLineItemRemovedEventHandler(object sender, Guid itemId);
}