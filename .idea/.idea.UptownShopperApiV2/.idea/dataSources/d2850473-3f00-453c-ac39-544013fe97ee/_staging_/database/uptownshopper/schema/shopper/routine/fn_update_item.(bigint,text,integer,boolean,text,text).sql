create function shopper.fn_update_item(_item_id bigint, _name text, _item_type integer, _active boolean, _notes text, _created_by text, _modified_by text) returns void
LANGUAGE plpgsql
AS $$
BEGIN
  UPDATE shopper.item
  SET
    name = _name,
    item_type = _item_type,
    active = _active,
    notes = _notes,
    date_modified = public.getdate(),
    created_by = _created_by,
    modified_by = _modified_by
  WHERE id = _item_id;
END
$$;
