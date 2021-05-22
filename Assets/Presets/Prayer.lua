local cmd = _cmd();


function angle (limbO, limbI, desiredAngle)
  limbO.MotorSpeed = desiredAngle - limbI.Angle;
end

function snap (limbO, limbI, offset)
  local t = (offset + _time.realtimeSinceStartup) % 5;
  if t > 2 then
    limbO.MotorSpeed = -40 * (1 + _body.Rotation);
  else
    angle(limbO, limbI, 90)
  end
end

snap(cmd.Limbs.Facing, _limbs.Facing, 0)
snap(cmd.Limbs.OuterFacing, _limbs.OuterFacing, 2)
angle(cmd.Limbs.Away, _limbs.Away, 10*math.sin(_time.realtimeSinceStartup) - 60)
angle(cmd.Limbs.OuterAway, _limbs.OuterAway, -90*math.sin(_time.realtimeSinceStartup))

return cmd;