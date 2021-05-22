local cmd = _cmd();


function angle (limbO, limbI, desiredAngle)
  limbO.MotorSpeed = desiredAngle - limbI.Angle;
end

function snap (limbO, limbI)
  local t = _time.realtimeSinceStartup % 10;
  if t > 8 then
    limbO.MotorSpeed = -9000;
  else
    angle(limbO, limbI, 90)
  end
end

snap(cmd.Limbs.Away, _limbs.Away)
snap(cmd.Limbs.OuterAway, _limbs.OuterAway)
angle(cmd.Limbs.Facing, _limbs.Facing, 10*math.sin(_time.realtimeSinceStartup) - 40)
angle(cmd.Limbs.OuterFacing, _limbs.OuterFacing, 10*math.sin(_time.realtimeSinceStartup) - 70)

return cmd;