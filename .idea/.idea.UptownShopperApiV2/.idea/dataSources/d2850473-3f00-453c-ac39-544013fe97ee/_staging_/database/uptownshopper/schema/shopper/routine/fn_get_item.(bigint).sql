create function shopper.fn_get_item(_item_id bigint) returns TABLE(id bigint, name character varying, item_type integer, active boolean, notes character varying, created_by character varying, modified_by character varying)
LANGUAGE plpgsql
AS $$
BEGIN
  RETURN QUERY
  select
    i.id,
    i.name,
    i.item_type,
    i.active,
    i.notes,
    i.created_by,
    i.modified_by
  FROM shopper.item i
  WHERE i.date_deleted is null
        AND (i.id = _item_id or nullif(_item_id, null) is null)
  ORDER BY name ASC;
END
$$;
